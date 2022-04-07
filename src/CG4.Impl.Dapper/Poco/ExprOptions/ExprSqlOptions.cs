using System.Linq.Expressions;
using CG4.Impl.Dapper.Poco.Expressions;

namespace CG4.Impl.Dapper.Poco.ExprOptions
{
    public class ExprSqlOptions<TEntity> : IClassSqlOptions<TEntity>
        where TEntity : class
    {
        public ExprSqlOptions(string alias)
        {
            Alias = alias;
            var map = PocoHub.GetMap<TEntity>();

            Sql = new ExprSql
            {
                Select = new ExprSelect(),
                From = new ExprFrom
                {
                    TableName = new() { Alias = alias, TableName = map.TableName },
                },
                Where = new ExprWhere(),
                OrderBy = new ExprOrderBy(),
            };

            foreach (var p in map.Properties)
            {
                Sql.Select.Add(new() { Alias = alias, Name = p.ColumnName, ResultName = p.Name });
            }
        }

        internal ExprSql Sql { get; set; }

        public string Alias { get; }

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
            var expr = SqlExprHelper.GenerateColumn(keySelector, Alias);
            Sql.OrderBy.Add(new ExprOrderColumn(expr, true));

            return this;
        }

        public IClassSqlOptions<TEntity> OrderByDesc<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            var expr = SqlExprHelper.GenerateColumn(keySelector, Alias);
            Sql.OrderBy.Add(new ExprOrderColumn(expr, false));

            return this;
        }

        public IClassSqlOptions<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var expr = SqlExprHelper.GenerateWhere(predicate, Alias);
            Sql.Where.And(expr);

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
    }
}
