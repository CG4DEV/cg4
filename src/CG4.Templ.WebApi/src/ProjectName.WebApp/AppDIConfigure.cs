using CG4.DataAccess;
using CG4.DataAccess.Poco;
using CG4.Executor.Extensions;
using CG4.Executor.Story;
using CG4.Extensions;
using CG4.Impl.Dapper.Crud;
using CG4.Impl.Dapper.Poco.ExprOptions;
using CG4.Impl.Dapper.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.Common;
using ProjectName.Common.Impl;
using ProjectName.Story;

namespace ProjectName.WebApp
{
    public static class AppDIConfigure
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            services.AddExecutors(options =>
            {
                options.ExecutionTypes = new[] { typeof(IStory<>), typeof(IStory<,>) };
                options.ExecutorInterfaceType = typeof(IStoryExecutor);
                options.ExecutorImplementationType = typeof(StoryExecutor);
            }, typeof(IStoryLibrary));

            services.AddSingleton<IAppSettings, IConnectionSettings, AppSettings>();
            services.AddSingleton<IConnectionFactory, ConnectionFactoryPostgreSQL>();
            services.AddSingleton<ISqlBuilder, ExprSqlBuilder>();
            services.AddSingleton<ISqlSettings, PostreSqlOptions>();
            
            services.AddScoped<ICrudService, AppCrudService>();
            services.AddScoped<IAppCrudService, AppCrudService>();

            services.AddTransient<ISearchService, DbSearchService>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
