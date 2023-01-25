using System;
using CG4.DataAccess;
using CG4.DataAccess.Poco;
using CG4.Extensions;
using CG4.Impl.Dapper.Crud;
using CG4.Impl.Dapper.Poco.ExprOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.Common;
using ProjectName.Common.Impl;
using ProjectName.Core.Common;
using ProjectName.Core.Common.Impl;
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

            services
                .AddScoped<ICrudService, AppCrudService>()
                .AddScoped<IAppCrudService, AppCrudService>()
                .AddScoped<ISearchService, DbSearchService>();

            services
                .AddSingleton<IConnectionFactory, ProjectNameConnectionFactory>()
                .AddSingleton<ISqlBuilder, ExprSqlBuilder>()
                .AddSingleton<ISqlSettings, PostreSqlOptions>();

            return services.BuildServiceProvider();
        }

        private static string GetEnvironment() =>
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
    }
}
