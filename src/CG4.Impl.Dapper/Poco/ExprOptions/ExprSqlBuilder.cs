using System.Linq.Expressions;
using System.Text;
using CG4.Impl.Dapper.Poco.Expressions;

namespace CG4.Impl.Dapper.Poco.ExprOptions
{
    public class ExprSqlBuilder : ISqlBuilder
    {
        internal const string ALIAS = "t";
        private const string ID = "Id";
        private const string SQL_ID = "id";

        private readonly ISqlSettings _sqlSettings;

        public ExprSqlBuilder(ISqlSettings sqlOptions)
        {
            _sqlSettings = sqlOptions;
        }

        public string DeleteById<T>()
            where T : class
        {
            var map = PocoHub.GetMap<T>();
            return $"DELETE FROM {GetTableSqlName(map)} WHERE {GetSqlId()} = {BuildParametr(ID)}";
        }

        public string GetAll<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null)
            where T : class
        {
            var options = GetSqlOptionsResult(predicate);

            return GenerateSelectQuery(options.Sql);
        }

        public string GetById<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null)
            where T : class
        {
            var options = GetSqlOptionsResult(predicate);
            var exprById = new ExprColumn(ALIAS, SQL_ID) == new ExprParam { Name = ID };

            options.Sql.Where.And(exprById);

            return GenerateSelectQuery(options.Sql);
        }

        public string Insert<T>()
            where T : class
        {
            var props = GetProperties<T>();

            var massProp = props.Select(p => CoverName(p.ColumnName));

            var massValue = props.Select(p => BuildParametr(p.Name));

            return $"INSERT INTO {GetTableSqlName(PocoHub.GetMap<T>())} ({string.Join(", ", massProp)}) VALUES ({string.Join(", ", massValue)}) RETURNING {SQL_ID}";
        }

        public string UpdateById<T>()
            where T : class
        {
            var props = GetProperties<T>()
                .Select(x => $"{CoverName(x.ColumnName)} = {BuildParametr(x.Name)}");

            return $"UPDATE {GetTableSqlName(PocoHub.GetMap<T>())} SET {string.Join(',', props)} WHERE {GetSqlId()} = {BuildParametr(ID)}";
        }

        private string GenerateSelectQuery(ExprSql sql)
        {
            var sb = new StringBuilder();
            var visitor = new ExprPostgreSqlVisitor(sb);

            sql.Accept(visitor);

            return sb.ToString();
        }

        private ExprSqlOptionsResult GetSqlOptionsResult<T>(Expression<Action<IClassSqlOptions<T>>> predicate)
            where T : class
        {
            var sqlOptions = new ExprSqlOptions<T>(ALIAS);

            if (predicate == null)
            {
                return sqlOptions.GetResult();
            }

            var compiledExpression = predicate.Compile();
            compiledExpression.Invoke(sqlOptions);

            return sqlOptions.GetResult();
        }

        private IEnumerable<PropertyMap> GetProperties<T>()
            where T : class
        {
            var map = PocoHub.GetMap<T>();

            return map.Properties.Where(p => !p.IsIdentity && !p.IsIgnored);
        }

        private string GetTableSqlName(ClassMap map)
        {
            return CoverName(map.TableName);
        }

        private string GetSqlId()
        {
            return CoverName(SQL_ID);
        }

        private string CoverName(string sqlObject)
        {
            return $"{_sqlSettings.StartDelimiter}{sqlObject}{_sqlSettings.EndDelimiter}";
        }

        private string BuildParametr(string paramName)
        {
            return $"{_sqlSettings.ParameterPrefix}{paramName}";
        }
    }
}
