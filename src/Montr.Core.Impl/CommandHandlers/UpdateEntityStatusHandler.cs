﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using MediatR;
using Montr.Core.Commands;
using Montr.Core.Impl.Entities;
using Montr.Core.Models;
using Montr.Core.Services;
using Montr.Data.Linq2Db;

namespace Montr.Core.Impl.CommandHandlers
{
	public class UpdateEntityStatusHandler : IRequestHandler<UpdateEntityStatus, ApiResult>
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		private readonly IDbContextFactory _dbContextFactory;

		public UpdateEntityStatusHandler(IUnitOfWorkFactory unitOfWorkFactory, IDbContextFactory dbContextFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
			_dbContextFactory = dbContextFactory;
		}

		public async Task<ApiResult> Handle(UpdateEntityStatus request, CancellationToken cancellationToken)
		{
			var item = request.Item ?? throw new ArgumentNullException(nameof(request.Item));

			using (var scope = _unitOfWorkFactory.Create())
			{
				using (var db = _dbContextFactory.Create())
				{
					var affected = await db.GetTable<DbEntityStatus>()
						.Where(x => x.EntityTypeCode == request.EntityTypeCode &&
									x.EntityUid == request.EntityUid &&
									x.Code == item.Code)
						.Set(x => x.Name, item.Name)
						.UpdateAsync(cancellationToken);

					scope.Commit();

					return new ApiResult { AffectedRows = affected };
				}
			}
		}
	}
}
