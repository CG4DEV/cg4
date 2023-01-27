using CG4.DataAccess;
using CG4.DataAccess.Poco;
using CG4.Extensions;
using CG4.Impl.Dapper.Crud;
using CG4.Impl.Dapper.Poco.ExprOptions;
using CG4.Impl.Kafka.Consumer;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.Common;
using ProjectName.Common.Impl;

namespace ProjectName.Consumer.WebApp
{
    public class DIConfigure
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            services.AddSingleton<IAppSettings, IConnectionSettings, IKafkaConsumerSettings, AppSettings>();

            services.AddTransient<ICrudService, AppCrudService>();
            services.AddTransient<IAppCrudService, AppCrudService>();

            services.AddSingleton<IConnectionFactory, ProjectNameConnectionFactory>();
            services.AddSingleton<ISqlBuilder, ExprSqlBuilder>();
            services.AddSingleton<ISqlSettings, PostreSqlOptions>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}