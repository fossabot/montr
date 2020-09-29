﻿using System;
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
	public class InsertEntityStatusHandler : IRequestHandler<InsertEntityStatus, ApiResult>
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		private readonly IDbContextFactory _dbContextFactory;

		public InsertEntityStatusHandler(IUnitOfWorkFactory unitOfWorkFactory, IDbContextFactory dbContextFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
			_dbContextFactory = dbContextFactory;
		}

		public async Task<ApiResult> Handle(InsertEntityStatus request, CancellationToken cancellationToken)
		{
			var item = request.Item ?? throw new ArgumentNullException(nameof(request.Item));

			using (var scope = _unitOfWorkFactory.Create())
			{
				using (var db = _dbContextFactory.Create())
				{
					var affected = await db.GetTable<DbEntityStatus>()
						.Value(x => x.EntityTypeCode, request.EntityTypeCode)
						.Value(x => x.EntityUid, request.EntityUid)
						.Value(x => x.Code, item.Code)
						.Value(x => x.Name, item.Name)
						.Value(x => x.DisplayOrder, item.DisplayOrder)
						.InsertAsync(cancellationToken);

					scope.Commit();

					return new ApiResult { AffectedRows = affected };
				}
			}
		}
	}
}
