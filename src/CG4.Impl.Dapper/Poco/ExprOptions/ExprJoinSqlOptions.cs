using System.Linq.Expressions;
using CG4.Impl.Dapper.Poco.Expressions;

namespace CG4.Impl.Dapper.Poco.ExprOptions
{
    public class ExprJoinSqlOptions<TEntity, TJoin> : IClassJoinSqlOptions<TEntity, TJoin>
        where TEntity : class
        where TJoin : class
    {
        private readonly ExprSqlOptions<TEntity> _exprSqlOptions;

        private ExprJoinSqlOptions(ExprSqlOptions<TEntity> exprSqlOptions, string alias)
        {
            _exprSqlOptions = exprSqlOptions;
            Alias = alias;
        }

        public static ExprJoinSqlOptions<TEntity, TJoin> CreateJoin<TKey>(
            ExprSqlOptions<TEntity> exprSqlOptions,
            Expression<Func<TEntity, TKey>> keySelector,
            string alias)
        {
            return Create(exprSqlOptions, keySelector, alias, new ExprJoin());
        }

        public static ExprJoinSqlOptions<TEntity, TJoin> CreateInnerJoin<TKey>(
            ExprSqlOptions<TEntity> exprSqlOptions,
            Expression<Func<TEntity, TKey>> keySelector,
            string alias)
        {
            return Create(exprSqlOptions, keySelector, alias, new ExprInnerJoin());
        }

        public static ExprJoinSqlOptions<TEntity, TJoin> CreateLeftJoin<TKey>(
            ExprSqlOptions<TEntity> exprSqlOptions,
            Expression<Func<TEntity, TKey>> keySelector,
            string alias)
        {
            return Create(exprSqlOptions, keySelector, alias, new ExprLeftJoin());
        }

        public static ExprJoinSqlOptions<TEntity, TJoin> CreateRightJoin<TKey>(
            ExprSqlOptions<TEntity> exprSqlOptions,
            Expression<Func<TEntity, TKey>> keySelector,
            string alias)
        {
            return Create(exprSqlOptions, keySelector, alias, new ExprRightJoin());
        }

        private static ExprJoinSqlOptions<TEntity, TJoin> Create<TKey>(
            ExprSqlOptions<TEntity> exprSqlOptions,
            Expression<Func<TEntity, TKey>> keySelector,
            string alias,
            ExprJoin exprJoin)
        {
            var expr = SqlExprHelper.GenerateColumn(keySelector);
            expr.Alias = exprSqlOptions.Alias;

            var mapEntity = PocoHub.GetMap<TEntity>();
            var mapJoin = PocoHub.GetMap<TJoin>();

            exprJoin.TableColumn = new ExprColumn { Alias = alias, Name = mapEntity.Properties.First(x => x.IsPrymaryKey || x.IsIdentity).ColumnName };
            exprJoin.OtherColumn = expr;
            exprJoin.TableName = new ExprTableName { Alias = alias, TableName = mapJoin.TableName };

            foreach (var p in mapJoin.Properties.Where(x => !x.IsIdentity && !x.IsPrymaryKey))
            {
                exprSqlOptions.Sql.Select.Add(new()
                {
                    Alias = alias,
                    Name = p.ColumnName,
                    ResultName = alias + p.Name,
                });
            }

            exprSqlOptions.Sql.From.Joins.Add(exprJoin);

            var join = new ExprJoinSqlOptions<TEntity, TJoin>(exprSqlOptions, alias)
            {
                JoinExpr = expr,
            };

            return join;
        }

        public ExprColumn JoinExpr { get; set; }

        public string Alias { get; }

        public Type GetCurrentType()
        {
            return typeof(TJoin);
        }

        public IClassJoinSqlOptions<TEntity, TJoin> OrderBy<TKey>(Expression<Func<TJoin, TKey>> keySelector)
        {
            var column = SqlExprHelper.GenerateColumn(keySelector, Alias);
            _exprSqlOptions.Sql.OrderBy.Add(new ExprOrderColumn(column, true));
            return this;
        }

        public IClassJoinSqlOptions<TEntity, TJoin> OrderByDesc<TKey>(Expression<Func<TJoin, TKey>> keySelector)
        {
            var column = SqlExprHelper.GenerateColumn(keySelector, Alias);
            _exprSqlOptions.Sql.OrderBy.Add(new ExprOrderColumn(column, false));
            return this;
        }

        public IClassSqlOptions<TEntity> ToBackMain()
        {
            return _exprSqlOptions;
        }

        public IClassJoinSqlOptions<TEntity, TJoin> Where(Expression<Func<TJoin, bool>> predicate)
        {
            var expr = SqlExprHelper.GenerateWhere(predicate, Alias);
            _exprSqlOptions.Sql.Where.And(expr);

            return this;
        }

        public IClassJoinSqlOptions<TEntity, TNewJoin> Join<TNewJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias) where TNewJoin : class
        {
            return ExprJoinSqlOptions<TEntity, TNewJoin>.CreateInnerJoin(_exprSqlOptions, predicate, alias);
        }

        public IClassJoinSqlOptions<TEntity, TNewJoin> JoinLeft<TNewJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias) where TNewJoin : class
        {
            return ExprJoinSqlOptions<TEntity, TNewJoin>.CreateLeftJoin(_exprSqlOptions, predicate, alias);
        }

        public IClassJoinSqlOptions<TEntity, TNewJoin> JoinRight<TNewJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias) where TNewJoin : class
        {
            return ExprJoinSqlOptions<TEntity, TNewJoin>.CreateRightJoin(_exprSqlOptions, predicate, alias);
        }
    }
}
