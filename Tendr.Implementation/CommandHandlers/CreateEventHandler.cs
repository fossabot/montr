﻿using System;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using MediatR;
using Montr.Core.Services;
using Montr.Data.Linq2Db;
using Tendr.Commands;
using Tendr.Implementation.Entities;
using Tendr.Models;

namespace Tendr.Implementation.CommandHandlers
{
	public class CreateEventHandler : IRequestHandler<CreateEvent, long>
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		private readonly IDbContextFactory _dbContextFactory;
		private readonly IDateTimeProvider _dateTimeProvider;

		public CreateEventHandler(IUnitOfWorkFactory unitOfWorkFactory, IDbContextFactory dbContextFactory,
			IDateTimeProvider dateTimeProvider)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
			_dbContextFactory = dbContextFactory;
			_dateTimeProvider = dateTimeProvider;
		}

		public async Task<long> Handle(CreateEvent request,
			CancellationToken cancellationToken)
		{
			if (request.UserUid == Guid.Empty)
				throw new InvalidOperationException("UserUid can't be empty guid.");

			if (request.Event.CompanyUid == Guid.Empty)
				throw new InvalidOperationException("CompanyUid can't be empty guid.");

			var now = _dateTimeProvider.GetUtcNow();

			var item = request.Event;

			using (var scope = _unitOfWorkFactory.Create())
			{
				using (var db = _dbContextFactory.Create())
				{
					var id = db.SelectSequenceNextValue<long>("event_id_seq");

					await db.GetTable<DbEvent>()
						.Value(x => x.Id, id)
						.Value(x => x.ConfigCode, item.ConfigCode)
						.Value(x => x.StatusCode, EventStatusCode.Draft)
						.Value(x => x.CompanyUid, item.CompanyUid)
						.Value(x => x.Name, item.Name)
						.Value(x => x.Description, item.Description)
						.InsertAsync(cancellationToken);

					scope.Commit();

					return id;
				}
			}
		}
	}
}
