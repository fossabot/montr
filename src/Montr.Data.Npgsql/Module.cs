﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Montr.Modularity;
using Npgsql.Logging;

namespace Montr.Data.Npgsql
{
	public class Module: IModule
	{
		public void ConfigureServices(IConfiguration configuration, IServiceCollection services)
		{
			NpgsqlLogManager.IsParameterLoggingEnabled = true;
			// NpgsqlLogManager.Provider = new ConsoleLoggingProvider(NpgsqlLogLevel.Debug, true, true);
		}
	}
}
