using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectName.Migrations
{
    class Program
    {
        private const string ENVIRONMENT_VARIABLE_NAME = "ASPNETCORE_ENVIRONMENT";
        private const string DATABASE_NAME = "ProjectName";
        
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices()
        {
            var env = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE_NAME);
            var config = new ConfigurationBuilder()
                .AddJsonFile($"Configs/connectionStrings.{env}.json")
                .AddEnvironmentVariables()
                .Build();

            return new ServiceCollection()
                .AddSingleton<IConfiguration>(config)
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres10_0()
                    .WithGlobalConnectionString(x =>
                        x.GetRequiredService<IConfiguration>().GetConnectionString(DATABASE_NAME))
                    .WithGlobalCommandTimeout(TimeSpan.FromMinutes(5))
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}