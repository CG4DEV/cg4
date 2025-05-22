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

        [Fact]
        public void GenerateWhere_EqualsExpression_ReturnsBoolEqPredicate()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.StrColumn == "123");

            Assert.NotNull(expr);
            var exprEqual = Assert.IsType<ExprBoolEqPredicate>(expr);
            Assert.NotNull(exprEqual.Value);
            var strValue = Assert.IsType<ExprStr>(exprEqual.Value);
            Assert.Equal("123", strValue.Value);
        }

        [Fact]
        public void GenerateWhere_NotEqualsExpression_ReturnsBoolNotEqPredicate()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.StrColumn != "123");

            Assert.NotNull(expr);
            var exprNotEqual = Assert.IsType<ExprBoolNotEqPredicate>(expr);
            Assert.NotNull(exprNotEqual.Value);
            var strValue = Assert.IsType<ExprStr>(exprNotEqual.Value);
            Assert.Equal("123", strValue.Value);
        }

        [Fact]
        public void GenerateWhere_GreaterThanExpression_ReturnsGreaterThan()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.IntColumn > 10);

            Assert.NotNull(expr);
            var exprGreater = Assert.IsType<ExprGreaterThan>(expr);
            Assert.NotNull(exprGreater.Value);
            var intValue = Assert.IsType<ExprInt>(exprGreater.Value);
            Assert.Equal(10, intValue.Value);
        }

        [Fact]
        public void GenerateWhere_LessThanExpression_ReturnsLessThan()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.IntColumn < 10);

            Assert.NotNull(expr);
            var exprLess = Assert.IsType<ExprLessThan>(expr);
            Assert.NotNull(exprLess.Value);
            var intValue = Assert.IsType<ExprInt>(exprLess.Value);
            Assert.Equal(10, intValue.Value);
        }

        [Fact]
        public void GenerateWhere_GreaterOrEqualExpression_ReturnsGreaterThanOrEq()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.IntColumn >= 10);

            Assert.NotNull(expr);
            var exprGreaterOrEqual = Assert.IsType<ExprGreaterThanOrEq>(expr);
            Assert.NotNull(exprGreaterOrEqual.Value);
            var intValue = Assert.IsType<ExprInt>(exprGreaterOrEqual.Value);
            Assert.Equal(10, intValue.Value);
        }

        [Fact]
        public void GenerateWhere_LessOrEqualExpression_ReturnsLessThanOrEq()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.IntColumn <= 10);

            Assert.NotNull(expr);
            var exprLessOrEqual = Assert.IsType<ExprLessThanOrEq>(expr);
            Assert.NotNull(exprLessOrEqual.Value);
            var intValue = Assert.IsType<ExprInt>(exprLessOrEqual.Value);
            Assert.Equal(10, intValue.Value);
        }

        [Fact]
        public void GenerateWhere_NullValueComparisonEqual_ReturnsBoolEqPredicateWithNullValue()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.StrColumn == null);

            Assert.NotNull(expr);
            var exprEqual = Assert.IsType<ExprBoolEqPredicate>(expr);
            Assert.NotNull(exprEqual.Value);
            Assert.IsType<ExprNull>(exprEqual.Value);
        }

        [Fact]
        public void GenerateWhere_NullValueComparisonNotEqual_ReturnsBoolNotEqPredicateWithNullValue()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.StrColumn != null);

            Assert.NotNull(expr);
            var exprNotEqual = Assert.IsType<ExprBoolNotEqPredicate>(expr);
            Assert.NotNull(exprNotEqual.Value);
            Assert.IsType<ExprNull>(exprNotEqual.Value);
        }

        [Fact]
        public void GenerateWhere_AndExpression_ReturnsBoolAnd()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.StrColumn != null && x.IntColumn > 10);

            Assert.NotNull(expr);
            var exprAnd = Assert.IsType<ExprBoolAnd>(expr);
            Assert.NotNull(exprAnd.Left);
            Assert.NotNull(exprAnd.Right);
            Assert.IsType<ExprBoolNotEqPredicate>(exprAnd.Left);
            Assert.IsType<ExprGreaterThan>(exprAnd.Right);
        }

        [Fact]
        public void GenerateWhere_OrExpression_ReturnsBoolOr()
        {
            var expr = ExprHelper.GenerateWhere<TestEntity>(x => x.StrColumn == "test" || x.IntColumn < 5);

            Assert.NotNull(expr);
            var exprOr = Assert.IsType<ExprBoolOr>(expr);
            Assert.NotNull(exprOr.Left);
            Assert.NotNull(exprOr.Right);
            Assert.IsType<ExprBoolEqPredicate>(exprOr.Left);
            Assert.IsType<ExprLessThan>(exprOr.Right);
        }
    }

    [Table("test_entity")]
    public class TestEntity : EntityBase
    {
        [Column("str_column")]
        public string? StrColumn { get; set; }

        [Column("int_column")]
        public int IntColumn { get; set; }
    }
}
