using CG4.DataAccess;
using CG4.DataAccess.Poco;
using CG4.Impl.Dapper.Poco.ExprOptions;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CG4.Impl.Dapper.PostgreSql
{
    /// <summary>
    /// Extension methods for configuring PostgreSQL database with Dapper in <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds PostgreSQL Dapper implementation to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configureDateTime">Configure legacy DateTime behavior for Npgsql and disable DateTime infinity conversions.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection AddPostgreSql(
            this IServiceCollection services,
            bool configureDateTime = false)
        {
            if (configureDateTime)
            {
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            }

            // Configure custom type handler for JsonArray to support JSONB data type in PostgreSQL
            SqlMapper.AddTypeHandler(new JsonArrayTypeHandler());
            
            // Register builder
            services.AddSingleton<ISqlBuilder, ExprSqlBuilder>();
            
            // Register settings
            services.AddSingleton<ISqlSettings, PostgreSqlOptions>();

            // Register connection factory
            services.AddSingleton<IConnectionFactory, ConnectionFactoryPostgreSQL>();
            
            // Register crud repository
            services.TryAddTransient<ICrudService, AppCrudService>();

            return services;
        }
    }
} 