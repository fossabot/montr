﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Montr.Core.Impl.Services;
using Montr.Core.Models;
using Montr.Core.Services;

namespace Montr.Core.Impl
{
	// ReSharper disable once UnusedMember.Global
	public class Module : IModule
	{
		public void ConfigureServices(IConfiguration configuration, IServiceCollection services)
		{
			services.AddTransient<IStartupTask, ImportDefaultLocaleStringListStartupTask>();

			services.AddSingleton<IMigrationRunner, DbMigrationRunner>();
			services.AddSingleton<EmbeddedResourceProvider, EmbeddedResourceProvider>();
			services.AddSingleton<LocaleStringSerializer, LocaleStringSerializer>();
			services.AddSingleton<ILocaleStringImporter, DbLocaleStringImporter>();
			services.AddSingleton<IRepository<LocaleString>, DbLocaleStringRepository>();
			services.AddSingleton<IAuditLogService, DbAuditLogService>();

			services.AddSingleton<IRepository<EntityStatus>, DbEntityStatusRepository>();
			services.AddSingleton<IEntityStatusProvider, DefaultEntityStatusProvider>();
		}
	}
}
