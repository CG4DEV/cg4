using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CG4.DataAccess.Domain;
using CG4.DataAccess.Poco;
using CG4.DataAccess.Poco.Expressions;
using Xunit;

namespace CG4.SqlBuilder.Tests
{
    public class SqlExprHelperTests
    {
        [Fact]
        public void GenerateWhere_ContainsExpression_ReturnsILikeExpression()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.StrColumn.Contains("123"));

            Assert.NotNull(expr);
            Assert.IsType<ExprLike>(expr);

            var exprLike = expr as ExprLike;

            Assert.Equal("123", exprLike.Value);
            Assert.Equal("%", exprLike.StartsPattern);
            Assert.Equal("%", exprLike.EndsPattern);
        }

        [Fact]
        public void GenerateWhere_StartsWithExpression_ReturnsILikeExpression()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.StrColumn.StartsWith("123"));

            Assert.NotNull(expr);
            Assert.IsType<ExprLike>(expr);

            var exprLike = expr as ExprLike;

            Assert.Equal("123", exprLike.Value);
            Assert.Equal(string.Empty, exprLike.StartsPattern);
            Assert.Equal("%", exprLike.EndsPattern);
        }

        [Fact]
        public void GenerateWhere_EndsWithExpression_ReturnsILikeExpression()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.StrColumn.EndsWith("123"));

            Assert.NotNull(expr);
            Assert.IsType<ExprLike>(expr);

            var exprLike = expr as ExprLike;

            Assert.Equal("123", exprLike.Value);
            Assert.Equal("%", exprLike.StartsPattern);
            Assert.Equal(string.Empty, exprLike.EndsPattern);
        }

        [Fact]
        public void GenerateWhere_ArrayContainsExpression_ReturnsInExpression()
        {
            var array = new string[] { "1", "2", "3" };
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => array.Contains(x.StrColumn));

            Assert.NotNull(expr);
            Assert.IsType<ExprIn>(expr);

            var exprIn = expr as ExprIn;

            Assert.NotEmpty(exprIn.Values);
        }

        [Fact]
        public void GenerateWhere_EnumerableContainsExpression_ReturnsInExpression()
        {
            var array = (new string[] { "1", "2", "3" }).Select(x => x);
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => array.Contains(x.StrColumn));

            Assert.NotNull(expr);
            Assert.IsType<ExprIn>(expr);

            var exprIn = expr as ExprIn;

            Assert.NotEmpty(exprIn.Values);
        }
    }

    [Table("test_entity")]
    public class TestEntity : EntityBase
    {
        [Column("str_column")]
        public string StrColumn { get; set; }
    }
}
