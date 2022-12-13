using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CG4.DataAccess.Domain;
using CG4.Impl.Dapper;
using CG4.Impl.Dapper.Crud;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectName.Integration.Tests
{
    public class DatabaseFixture
    {
        public ICrudService CrudService => _crudService;
        public IConnectionFactory ConnectionFactory => _connectionFactory;

        private readonly IConnectionFactory _connectionFactory;
        private readonly ICrudService _crudService;

        public DatabaseFixture()
        {
            var serviceProvider = Factory.GetServiceProvider();

            _crudService = serviceProvider.GetService<ICrudService>();
            _connectionFactory = serviceProvider.GetService<IConnectionFactory>();
        }

        public async Task CleanUpAsync(IEnumerable<EntityBase> listToRemove)
        {
            var sql = GetSQLForDelete(listToRemove);

            await CrudService.ExecuteAsync(sql);
        }

        public static string GetSQLForDelete(IEnumerable<EntityBase> listToRemove)
        {
            if (!listToRemove.Any())
            {
                return null;
            }

            var sqlList = new List<string>();

            foreach (var item in listToRemove)
            {
                var entityType = item.GetType();
                AppendDeleteSql(entityType, item.Id, sqlList);
            }

            return string.Join(Environment.NewLine, sqlList);
        }

        private static void AppendDeleteSql(Type entityType, long id, List<string> sqlList)
        {
            var attribute = entityType.GetCustomAttribute<TableAttribute>();

            if (attribute == null)
            {
                return;
            }

            var tableName = attribute.Name;
            var containsParentId = entityType
                .GetProperties()
                .SelectMany(x => x.GetCustomAttributes(typeof(ColumnAttribute), false))
                .Cast<ColumnAttribute>()
                .Any(x => x.Name == "parent_id");

            if (containsParentId)
            {
                sqlList.Add($"DELETE FROM {tableName} WHERE parent_id = {id};");
            }

            sqlList.Add($"DELETE FROM {tableName} WHERE id = {id};");

            if (entityType.BaseType != null)
            {
                AppendDeleteSql(entityType.BaseType, id, sqlList);
            }
        }
    }
}
