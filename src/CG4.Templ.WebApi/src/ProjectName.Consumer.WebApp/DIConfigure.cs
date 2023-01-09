using CG4.Executor.Extensions;
using CG4.Executor.Story;
using CG4.Extensions;
using CG4.Impl.Dapper;
using CG4.Impl.Dapper.Crud;
using CG4.Impl.Dapper.Poco;
using CG4.Impl.Dapper.Poco.ExprOptions;
using CG4.Impl.Kafka.Consumer;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.Consumer.Common;
using ProjectName.Consumer.Common.Impl;
using ProjectName.Consumer.Story;

namespace ProjectName.Consumer.WebApp
{
    public class DIConfigure
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            services.AddExecutors(options =>
            {
                var executionTypes = new[] { typeof(IStory<>), typeof(IStory<,>) };

                options.ExecutorInterfaceType = typeof(IStoryExecutor);
                options.ExecutorImplementationType = typeof(StoryExecutor);
                options.ExecutorLifetime = ServiceLifetime.Singleton;
                options.ExecutionTypes = executionTypes;
                options.ExecutionTypesLifetime = ServiceLifetime.Transient;
            }, typeof(IStoryLibrary).Assembly);

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