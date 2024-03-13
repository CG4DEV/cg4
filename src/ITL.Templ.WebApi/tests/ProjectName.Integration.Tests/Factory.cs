﻿using System;
using ITL.DataAccess;
using ITL.DataAccess.Poco;
using ITL.Extensions;
using ITL.Impl.Dapper.Crud;
using ITL.Impl.Dapper.Poco.ExprOptions;
using ITL.Impl.Dapper.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.WebApp;

namespace ProjectName.Integration.Tests
{
    public class Factory
    {
        public static IConfiguration GetConfiguration()
        {
            var env = GetEnvironment();

            return new ConfigurationBuilder()
                .AddJsonFile($"Configs/connectionStrings.{env}.json")
                .AddJsonFile($"Configs/serilog.{env}.json")
                .Build();
        }

        public static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton(GetConfiguration());
            services.AddSingleton<IAppSettings, IConnectionSettings, AppSettings>();
            services.AddSingleton<IConnectionFactory, ConnectionFactoryPostgreSQL>();
            services.AddSingleton<ISqlBuilder, ExprSqlBuilder>();
            services.AddSingleton<ISqlSettings, PostreSqlOptions>();

            services
                .AddScoped<ICrudService, AppCrudService>()
                .AddScoped<IAppCrudService, AppCrudService>();

            return services.BuildServiceProvider();
        }

        private static string GetEnvironment() =>
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
    }
}
