﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Montr.Core.Services;
using Montr.Data.Linq2Db;
using Montr.Idx.Impl.Entities;

namespace Montr.Idx.Impl.Services
{
	public class CreateDefaultAdminStartupTask : IStartupTask
	{
		private readonly ILogger<CreateDefaultAdminStartupTask> _logger;
		private readonly IOptionsMonitor<Options> _optionsAccessor;
		private readonly IDbContextFactory _dbContextFactory;
		private readonly UserManager<DbUser> _userManager;

		public CreateDefaultAdminStartupTask(
			ILogger<CreateDefaultAdminStartupTask> logger,
			IOptionsMonitor<Options> optionsAccessor,
			IDbContextFactory dbContextFactory,
			UserManager<DbUser> userManager)
		{
			_logger = logger;
			_optionsAccessor = optionsAccessor;
			_dbContextFactory = dbContextFactory;
			_userManager = userManager;
		}

		public async Task Run(CancellationToken cancellationToken)
		{
			var options = _optionsAccessor.CurrentValue;

			if (options?.DefaultAdminEmail != null && options.DefaultAdminPassword != null)
			{
				if (await _dbContextFactory.HasData<DbUser>(cancellationToken) == false)
				{
					_logger.LogInformation("Creating default administrator");

					var user = new DbUser
					{
						Id = Guid.NewGuid(),
						UserName = options.DefaultAdminEmail,
						Email = options.DefaultAdminEmail
					};

					var identityResult = await _userManager.CreateAsync(user, options.DefaultAdminPassword);

					if (identityResult.Succeeded == false)
					{
						var errors = string.Join(", ", identityResult.Errors.Select(x => x.Description));

						throw new ApplicationException($"Failed to create default administrator: {errors}");
					}
				}
			}
		}
	}
}
