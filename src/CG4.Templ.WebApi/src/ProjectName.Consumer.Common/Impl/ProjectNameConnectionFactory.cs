using CG4.Impl.Dapper;
using CG4.Impl.Dapper.PostgreSql;

namespace ProjectName.Consumer.Common.Impl
{
    public class ProjectNameConnectionFactory : ConnectionFactoryPostgreSQL, IConnectionFactory
    {
        public ProjectNameConnectionFactory(IConnectionSettings connectionSettings)
            : base(connectionSettings.ConnectionString)
        {
            
        }
    }
}