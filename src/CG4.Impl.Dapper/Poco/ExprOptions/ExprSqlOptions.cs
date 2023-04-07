using System.Linq.Expressions;
using CG4.DataAccess.Poco;
using CG4.DataAccess.Poco.Expressions;

namespace CG4.Impl.Dapper.Poco.ExprOptions
{
    public class ExprSqlOptions<TEntity> : IClassSqlOptions<TEntity>
        where TEntity : class
    {
        private readonly string _defAlias;
        private int _index = 0;

        private readonly List<ExprColumn> _columns = new();
        private readonly List<ExprBoolean> _booleans = new();

        public ExprSqlOptions(string defaultAlias)
        {
            _defAlias = defaultAlias;
            Alias = defaultAlias;
            var map = PocoHub.GetMap<TEntity>();

            Sql = new ExprSql
            {
                Select = new ExprSelect(),
                From = new ExprFrom
                {
                    TableName = new() { Alias = Alias, TableName = map.TableName },
                },
                Where = new ExprWhere(),
                OrderBy = new ExprOrderBy(),
            };

            foreach (var p in map.Properties.Where(x => !x.IsIgnored))
            {
                var col = new ExprSelectedColumn() { Alias = Alias, Name = p.ColumnName, ResultName = p.Name };
                _columns.Add(col);
                Sql.Select.Add(col);
            }
        }

        internal ExprSql Sql { get; set; }

        public string Alias { get; set; }

        internal string GetAlias() => $"{_defAlias}{_index++}";

        public Type GetCurrentType()
        {
            return typeof(TEntity);
        }

        public ExprSqlOptionsResult GetResult()
        {
            return new ExprSqlOptionsResult
            {
                Sql = Sql,
            };
        }

        public IClassSqlOptions<TEntity> As(string tableAlias)
        {
            Alias = tableAlias;
            Sql.From.TableName.Alias = tableAlias;

            foreach (var item in _columns)
            {
                item.Alias = tableAlias;
            }

            foreach (var item in _booleans)
            {
                SqlExprHelper.SetAlias(item, tableAlias);
            }

            return this;
        }

        public IClassJoinSqlOptions<TEntity, TJoin> Join<TJoin, TKey>(Expression<Func<TEntity, TKey>> keySelector, string alias)
            where TJoin : class
        {
            var join = ExprJoinSqlOptions<TEntity, TJoin>.CreateInnerJoin<TKey>(this, keySelector, alias);

            return join;
        }

        public IClassJoinSqlOptions<TEntity, TJoin> JoinLeft<TJoin, TKey>(Expression<Func<TEntity, TKey>> keySelector, string alias)
            where TJoin : class
        {
            var join = ExprJoinSqlOptions<TEntity, TJoin>.CreateLeftJoin<TKey>(this, keySelector, alias);

            return join;
        }

        public IClassJoinSqlOptions<TEntity, TJoin> JoinRight<TJoin, TKey>(Expression<Func<TEntity, TKey>> keySelector, string alias)
            where TJoin : class
        {
            var join = ExprJoinSqlOptions<TEntity, TJoin>.CreateRightJoin<TKey>(this, keySelector, alias);

            return join;
        }

        public IClassSqlOptions<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return OrderByInternal(keySelector, true);
        }

        public IClassSqlOptions<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool ask)
        {
            return OrderByInternal(keySelector, ask);
        }

        public IClassSqlOptions<TEntity> OrderByDesc<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return OrderByInternal(keySelector, false);
        }

        public IClassSqlOptions<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var expr = SqlExprHelper.GenerateWhere(predicate, Alias);
            _booleans.Add(expr);
            Sql.Where.And(expr);

            return this;
        }

        public IClassSqlOptions<TEntity> Where(ExprBoolean predicate)
        {
            SqlExprHelper.SetAlias(predicate, Alias);
            _booleans.Add(predicate);
            Sql.Where.And(predicate);

            return this;
        }

        /// <summary>
        /// Append where conditions without alias updates
        /// </summary>
        /// <param name="predicate">Additional conditions</param>
        public IClassSqlOptions<TEntity> AppendWhere(ExprBoolean predicate)
        {
            Sql.Where.And(predicate);
            return this;
        }

        public IClassSqlOptions<TEntity> Offset(int offset)
        {
            Sql.Offset = offset;
            return this;
        }

        public IClassSqlOptions<TEntity> Limit(int limit)
        {
            Sql.Limit = limit;
            return this;
        }

        private IClassSqlOptions<TEntity> OrderByInternal<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool ask)
        {
            var column = SqlExprHelper.GenerateColumn(keySelector, Alias);
            var order = new ExprOrderColumn(column, ask);
            _columns.Add(order);
            Sql.OrderBy.Add(order);
            return this;
        }
    }
}
