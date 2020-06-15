﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using Montr.Automate.Impl.Entities;
using Montr.Automate.Models;
using Montr.Core.Models;
using Montr.Core.Services;
using Montr.Data.Linq2Db;

namespace Montr.Automate.Impl.Services
{
	public class DbAutomationRepository : IRepository<Automation>
	{
		private readonly IDbContextFactory _dbContextFactory;

		public DbAutomationRepository(IDbContextFactory dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
		}

		public async Task<SearchResult<Automation>> Search(SearchRequest searchRequest, CancellationToken cancellationToken)
		{
			var request = (AutomationSearchRequest)searchRequest ?? throw new ArgumentNullException(nameof(searchRequest));

			using (var db = _dbContextFactory.Create())
			{
				var query = db.GetTable<DbAutomation>()
					.Where(x => x.EntityTypeCode == request.EntityTypeCode &&
								x.EntityTypeUid == request.EntityTypeUid);

				if (request.Uid != null)
				{
					query = query.Where(x => x.Uid == request.Uid);
				}

				if (request.IsActive != null)
				{
					query = query.Where(x => x.IsActive == request.IsActive);
				}

				if (request.IsSystem != null)
				{
					query = query.Where(x => x.IsSystem == request.IsSystem);
				}

				var paged = query.Apply(request, x => x.DisplayOrder);

				var data = await paged
					.Select(x => x)
					.ToListAsync(cancellationToken);

				var result = new List<Automation>();

				foreach (var dbItem in data)
				{
					var item = new Automation
					{
						Uid = dbItem.Uid,
						DisplayOrder = dbItem.DisplayOrder,
						Name = dbItem.Name,
						Description = dbItem.Description,
						Active = dbItem.IsActive,
						System = dbItem.IsSystem,
						Conditions = new List<AutomationCondition>(),
						Actions = new List<AutomationAction>()
					};

					result.Add(item);
				}

				return new SearchResult<Automation>
				{
					TotalCount = query.GetTotalCount(request),
					Rows = result
				};
			}
		}

		public Task<SearchResult<Automation>> Search2(SearchRequest searchRequest, CancellationToken cancellationToken)
		{
			var request = (AutomationSearchRequest)searchRequest ?? throw new ArgumentNullException(nameof(searchRequest));

			var result = new SearchResult<Automation>();

			if (request.EntityTypeCode == "DocumentType" &&
				request.EntityTypeUid == Guid.Parse("ab770d9f-f723-4468-8807-5df0f6637cca"))
			{
				result.Rows = new[]
				{
					new Automation
					{
						Active = true,
						Name = "Рассылка уведомлений на публикацию",
						Conditions = new List<AutomationCondition>
						{
							new FieldAutomationCondition
							{
								Field = "StatusCode",
								Operator = AutomationConditionOperator.Equal,
								Value = "Published"
							}
						},
						Actions = new List<AutomationAction>
						{
							new NotifyByEmailAutomationAction
							{
								Recipient = "operator",
								Subject = "New company registration request {{DocumentNumber}} from {{DocumentDate}} published",
								Body = "New company registration request {{DocumentNumber}} from {{DocumentDate}} published, please review."
							},
							new NotifyByEmailAutomationAction
							{
								Recipient = "requester",
								Subject = "Your company registration request {{DocumentNumber}} from {{DocumentDate}} received",
								Body = "Your company registration request {{DocumentNumber}} from {{DocumentDate}} received and will be reviewed."
							}
						}
					}
				};
			}

			return Task.FromResult(result);
		}
	}
}
