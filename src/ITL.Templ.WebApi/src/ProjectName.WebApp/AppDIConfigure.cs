using ITL.DataAccess;
using ITL.DataAccess.Poco;
using ITL.Executor.Extensions;
using ITL.Executor.Story;
using ITL.Extensions;
using ITL.Impl.Dapper.Crud;
using ITL.Impl.Dapper.Poco.ExprOptions;
using ITL.Impl.Dapper.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.Story;

namespace ProjectName.WebApp
{
    public static class AppDIConfigure
    {
        public static IServiceCollection ConfigureApp(this IServiceCollection services)
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

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
