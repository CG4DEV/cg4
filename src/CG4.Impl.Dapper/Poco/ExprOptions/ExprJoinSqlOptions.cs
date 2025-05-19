using System.Linq.Expressions;
using CG4.DataAccess.Poco;
using CG4.DataAccess.Poco.Expressions;

namespace CG4.Impl.Dapper.Poco.ExprOptions
{
    public class ExprJoinSqlOptions<TEntity, TJoin> : IClassJoinSqlOptions<TEntity, TJoin>
        where TEntity : class
        where TJoin : class
    {
        private readonly ExprSqlOptions<TEntity> _exprSqlOptions;

        private readonly List<ExprColumn> _columns = new();
        private readonly List<ExprBoolean> _booleans = new();
        private readonly ExprJoin _join;

        private ExprJoinSqlOptions(ExprSqlOptions<TEntity> exprSqlOptions, string alias, ExprJoin exprJoin)
        {
            _exprSqlOptions = exprSqlOptions;
            Alias = alias;
            _join = exprJoin;
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
            var expr = ExprHelper.GenerateColumn(keySelector);
            expr.Alias = exprSqlOptions.Alias;

            var mapEntity = PocoHub.GetMap<TEntity>();
            var mapJoin = PocoHub.GetMap<TJoin>();

            var aliasJoin = exprSqlOptions.GetAlias();

            exprJoin.TableColumn = new ExprColumn(aliasJoin, mapEntity.Properties.First(x => x.IsPrymaryKey || x.IsIdentity).ColumnName);
            exprJoin.OtherColumn = expr;
            exprJoin.TableName = new ExprTableName { Alias = aliasJoin, TableName = mapJoin.TableName };

            var join = new ExprJoinSqlOptions<TEntity, TJoin>(exprSqlOptions, aliasJoin, exprJoin);

            foreach (var p in mapJoin.Properties.Where(x => !x.IsIdentity && !x.IsPrymaryKey && !x.IsIgnored))
            {
                var col = new ExprSelectedColumn()
                {
                    Alias = aliasJoin,
                    Name = p.ColumnName,
                    ResultName = alias + p.Name,
                };

                join._columns.Add(col);
                exprSqlOptions.Sql.Select.Add(col);
            }

            exprSqlOptions.Sql.From.Joins.Add(exprJoin);

            return join;
        }

        public string Alias { get; set; }

        public Type GetCurrentType()
        {
            return typeof(TJoin);
        }

        public IClassJoinSqlOptions<TEntity, TJoin> As(string tableAlias)
        {
            Alias = tableAlias;
            _join.TableColumn.Alias = tableAlias;
            _join.TableName.Alias = tableAlias;

            foreach (var item in _columns)
            {
                item.Alias = tableAlias;
            }

            foreach (var item in _booleans)
            {
                ExprHelper.SetAlias(item, tableAlias);
            }

            return this;
        }

        public IClassJoinSqlOptions<TEntity, TJoin> OrderBy<TKey>(Expression<Func<TJoin, TKey>> keySelector)
        {
            return OrderByInternal(keySelector, true);
        }

        public IClassJoinSqlOptions<TEntity, TJoin> OrderBy<TKey>(Expression<Func<TJoin, TKey>> keySelector, bool ask)
        {
            return OrderByInternal(keySelector, ask);
        }

        public IClassJoinSqlOptions<TEntity, TJoin> OrderByDesc<TKey>(Expression<Func<TJoin, TKey>> keySelector)
        {
            return OrderByInternal(keySelector, false);
        }

        public IClassSqlOptions<TEntity> ToBackMain()
        {
            return _exprSqlOptions;
        }

        public IClassJoinSqlOptions<TEntity, TJoin> Where(Expression<Func<TJoin, bool>> predicate)
        {
            var expr = ExprHelper.GenerateWhere(predicate, Alias);

            _booleans.Add(expr);
            _exprSqlOptions.Sql.Where.And(expr);

            return this;
        }

        public IClassJoinSqlOptions<TEntity, TJoin> Where(ExprBoolean predicate)
        {
            ExprHelper.SetAlias(predicate, Alias);
            _booleans.Add(predicate);
            _exprSqlOptions.Sql.Where.And(predicate);

            return this;
        }

        /// <summary>
        /// Append where conditions without alias updates
        /// </summary>
        /// <param name="predicate">Additional conditions</param>
        public IClassJoinSqlOptions<TEntity, TJoin> AppendWhere(ExprBoolean predicate)
        {
            _exprSqlOptions.Sql.Where.And(predicate);
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

        private IClassJoinSqlOptions<TEntity, TJoin> OrderByInternal<TKey>(Expression<Func<TJoin, TKey>> keySelector, bool ask)
        {
            var column = ExprHelper.GenerateColumn(keySelector, Alias);
            var order = new ExprOrderColumn(column, ask);
            _columns.Add(order);
            _exprSqlOptions.Sql.OrderBy.Add(order);
            return this;
        }
    }
}
