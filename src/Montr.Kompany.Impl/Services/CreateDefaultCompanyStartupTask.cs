﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Montr.Core.Services;
using Montr.Data.Linq2Db;
using Montr.Idx.Models;
using Montr.Kompany.Commands;
using Montr.Kompany.Impl.Entities;
using Montr.Kompany.Models;

namespace Montr.Kompany.Impl.Services
{
	public class CreateDefaultCompanyStartupTask : IStartupTask
	{
		private readonly ILogger<CreateDefaultCompanyStartupTask> _logger;
		private readonly IOptionsMonitor<Options> _optionsAccessor;
		private readonly IRepository<User> _userRepository;
		private readonly IDbContextFactory _dbContextFactory;
		private readonly IMediator _mediator;

		public CreateDefaultCompanyStartupTask(
			ILogger<CreateDefaultCompanyStartupTask> logger,
			IOptionsMonitor<Options> optionsAccessor,
			IDbContextFactory dbContextFactory,
			IRepository<User> userRepository,
			IMediator mediator)
		{
			_logger = logger;
			_optionsAccessor = optionsAccessor;
			_dbContextFactory = dbContextFactory;
			_userRepository = userRepository;
			_mediator = mediator;
		}

		public async Task Run(CancellationToken cancellationToken)
		{
			var options = _optionsAccessor.CurrentValue;

			// if default company name specified...
			if (options?.DefaultCompanyName == null)
			{
				_logger.LogDebug("Default company name is not specified in startup options");

				return;
			}

			// ... and no companies in db yet...
			if (await _dbContextFactory.HasData<DbCompany>(cancellationToken))
			{
				_logger.LogWarning("Database already contains companies, consider to remove default company name from startup options");

				return;
			}

			var users = await _userRepository
				.Search(new UserSearchRequest { PageSize = 2, SkipPaging = true }, cancellationToken);

			// ... and only one user exists in db (default administrator)
			if (users.Rows.Count != 1)
			{
				_logger.LogDebug("Database already contains more than one user, skipping creating default company");

				return;
			}

			var user = users.Rows[0];

			_logger.LogInformation("Creating default company {companyName} for {userName}", options.DefaultCompanyName, user.UserName);

			var result = await _mediator.Send(new CreateCompany
			{
				// ReSharper disable once PossibleInvalidOperationException
				UserUid = user.Uid.Value,
				Item = new Company
				{
					Name = options.DefaultCompanyName,
					ConfigCode = CompanyConfigCode.Company // todo: register and use allowed company types
				}
			}, cancellationToken);

			result.AssertSuccess(() => "Failed to create default company");
		}
	}
}
