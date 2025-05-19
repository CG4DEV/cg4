using System.Linq.Expressions;
using System.Text;
using CG4.DataAccess.Poco;
using CG4.DataAccess.Poco.Expressions;

namespace CG4.Impl.Dapper.Poco.ExprOptions
{
    public class ExprSqlBuilder : ISqlBuilder
    {
        private const string ALIAS = "t";
        private const string ID = "Id";
        private const string SQL_ID = "id";
        private const string SEPARATOR = ", ";
        private const string SQL_DELETE = "DELETE FROM";
        private const string SQL_INSERT = "INSERT INTO";
        private const string SQL_UPDATE = "UPDATE";
        private const string SQL_SET = "SET";
        private const string SQL_WHERE = "WHERE";
        private const string SQL_RETURNING = "RETURNING";
        private const string SQL_VALUES = "VALUES";
        private const string SQL_AS = "AS";
        private const string SQL_FROM = "FROM";
        private const string SQL_SELECT = "SELECT";
        private const string SQL_INNER_JOIN = "INNER JOIN";
        private const string SQL_ON = "ON";
        private const string SQL_ORDER_BY = "ORDER BY";
        private const string SQL_ASC = "ASC";
        private const string SQL_DESC = "DESC";
        private const string SQL_LIMIT = "LIMIT";
        private const string SQL_OFFSET = "OFFSET";
        private const string SQL_AND = "AND";
        private const string SQL_OR = "OR";
        private const string SQL_IS_NULL = "IS NULL";
        private const string SQL_IS_NOT_NULL = "IS NOT NULL";
        private const string SQL_TRUE = "TRUE";
        private const string SQL_FALSE = "FALSE";

        private readonly ISqlSettings _sqlSettings;

        public ExprSqlBuilder(ISqlSettings sqlOptions)
        {
            _sqlSettings = sqlOptions ?? throw new ArgumentNullException(nameof(sqlOptions));
        }
        
        public string Insert<T>()
            where T : class
        {
            var props = GetProperties<T>();

            var massProp = props.Select(p => CoverName(p.ColumnName));
            var massValue = props.Select(p => BuildParametr(p.Name));

            return $"{SQL_INSERT} {GetTableSqlName(PocoHub.GetMap<T>())} ({string.Join(SEPARATOR, massProp)}) {SQL_VALUES} ({string.Join(SEPARATOR, massValue)}) {SQL_RETURNING} {SQL_ID}";
        }

        public string UpdateById<T>()
            where T : class
        {
            var props = GetProperties<T>()
                .Where(x => x.AllowEdit)
                .Select(x => $"{CoverName(x.ColumnName)} = {BuildParametr(x.Name)}");

            return $"{SQL_UPDATE} {GetTableSqlName(PocoHub.GetMap<T>())} {SQL_SET} {string.Join(SEPARATOR, props)} {SQL_WHERE} {GetSqlId()} = {BuildParametr(ID)}";
        }

        public string DeleteById<T>()
            where T : class
        {
            var map = PocoHub.GetMap<T>();
            return $"{SQL_DELETE} {GetTableSqlName(map)} {SQL_WHERE} {GetSqlId()} = {BuildParametr(ID)}";
        }

        public string GetById<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null)
           where T : class
        {
            var options = GetSqlOptionsResult(predicate);
            var exprById = new ExprColumn(ALIAS, SQL_ID) == new ExprParam { Name = ID };

            options.Sql.Where.And(exprById);

            return GenerateSelectQuery(options.Sql);
        }

        public string GetAll<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null)
            where T : class
        {
            var options = GetSqlOptionsResult(predicate);
            return GenerateSelectQuery(options.Sql);
        }

        public string Count<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null)
            where T : class
        {
            var options = GetSqlOptionsResult(predicate);

            var countFunc = new ExprFunctionCount();
            countFunc.Parametrs.Add(new ExprStar());

            options.Sql.Select = new ExprSelect
            {
                countFunc
            };

            options.Sql.Limit = null;
            options.Sql.Offset = null;

            return GenerateSelectQuery(options.Sql);
        }

        public ExprSql GenerateSql<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null)
            where T : class
        {
            var options = GetSqlOptionsResult(predicate);
            return options.Sql;
        }

        public string Serialize(ExprSql sql)
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            return GenerateSelectQuery(sql);
        }

        private string GenerateSelectQuery(ExprSql sql)
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            var sb = new StringBuilder();
            var visitor = new ExprSqlVisitor(sb);

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
            if (string.IsNullOrEmpty(sqlObject))
            {
                throw new ArgumentException("SQL object name cannot be null or empty", nameof(sqlObject));
            }

            return $"{_sqlSettings.StartDelimiter}{sqlObject}{_sqlSettings.EndDelimiter}";
        }

        private string BuildParametr(string paramName)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentException("Parameter name cannot be null or empty", nameof(paramName));
            }

            return $"{_sqlSettings.ParameterPrefix}{paramName}";
        }
    }
}
