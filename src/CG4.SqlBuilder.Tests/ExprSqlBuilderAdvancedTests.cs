using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CG4.DataAccess.Domain;
using CG4.DataAccess.Poco;
using CG4.Impl.Dapper.Poco.ExprOptions;
using Xunit;

namespace CG4.SqlBuilder.Tests
{
    public class ExprSqlBuilderAdvancedTests 
    {
        private readonly ISqlSettings _sqlSettings = new PostgreSqlOptions();

        [Fact]
        public void GetAll_WithComplexLikePatterns_ReturnCorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var value = "test%_value";  // Contains special characters % and _

            var sql = builder.GetAll<TestEntity>(x => 
                x.Where(p => p.Code.StartsWith(value))
                 .Where(p => p.Code.EndsWith(value))
                 .Where(p => p.Code.Contains(value)));

            Assert.NotNull(sql);
            Assert.Contains(@"WHERE t.""code"" LIKE 'test\%\_value%'", sql);
            Assert.Contains(@"AND t.""code"" LIKE '%test\%\_value'", sql);
            Assert.Contains(@"AND t.""code"" LIKE '%test\%\_value%'", sql);
        }

        [Fact]
        public void GetAll_WithMultipleAggregations_ReturnCorrectSql() 
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetAll<TestEntity>(x =>
                x.GroupBy(p => p.Code)
                 .Having(g => g.Number));

            Assert.NotNull(sql);
            Assert.Contains(@"GROUP BY t.""code""", sql);
            Assert.Contains(@"HAVING COUNT(*) > 1 AND MAX(t.""number"") > 100", sql);
            Assert.Contains(@"COUNT(*)", sql);
            Assert.Contains(@"SUM(t.""number"")", sql); 
            Assert.Contains(@"AVG(t.""number"")", sql);
            Assert.Contains(@"MIN(t.""number"")", sql);
            Assert.Contains(@"MAX(t.""number"")", sql);
        }

        //[Fact]
        //public void GetAll_WithExistsSubquery_ReturnCorrectSql()
        //{
        //    var builder = new ExprSqlBuilder(_sqlSettings);

        //    var subquery = builder.GetAll<TestSecondEntity>(x =>
        //        x.Where(p => p.Age > 18 && p.Name != null));

        //    var sql = builder.GetAll<TestEntity>(x =>
        //        x.Where(p => p..SecondId.Exists(subquery)));

        //    Assert.NotNull(sql);
        //    Assert.Contains(@"WHERE EXISTS (SELECT t.""id"" FROM ""test_second_entity"" AS t WHERE t.""age"" > 18 AND t.""name"" IS NOT NULL)", sql);
        //}

        //[Fact]
        //public void GetAll_WithCorrelatedSubquery_ReturnCorrectSql()
        //{
        //    var builder = new ExprSqlBuilder(_sqlSettings);

        //    var sql = builder.GetAll<TestEntity>(x =>
        //        x.Where(p => p.Number > p.All(s => 
        //            s.From<TestSecondEntity>()
        //             .Where(e => e.Age > 18)
        //             .Select(e => e.Id))));

        //    Assert.NotNull(sql);
        //    Assert.Contains(@"WHERE t.""number"" > ALL (SELECT s.""id"" FROM ""test_second_entity"" AS s WHERE s.""age"" > 18)", sql);
        //}

        [Fact]
        public void GetAll_WithMultipleLeftJoins_ReturnCorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetAll<TestEntity>(x =>
                x.JoinLeft<TestSecondEntity, long>(j => j.SecondId, "Second").As("s")
                 .JoinLeft<TestThirdEntity, long>(j => j.ThirdId, "Third").As("th")
                 .Where(p => p.Code3 != null && p.Name3 != null && p.Description3 != null));

            Assert.NotNull(sql);
            Assert.Contains(@"LEFT JOIN ""test_second_entity"" AS s ON s.""id"" = t.""test_second_entity_id""", sql);
            Assert.Contains(@"LEFT JOIN ""test_third_entity"" AS th ON th.""id"" = t.""test_third_entity_id""", sql);
            Assert.Contains(@"WHERE t.""code"" IS NOT NULL AND s.""name"" IS NOT NULL AND th.""description"" IS NOT NULL", sql);
        }

        [Table("test_entity")]
        private class TestEntity : EntityBase
        {
            [Column("code")]
            public string? Code { get; set; }

            [Column("number")]
            public int Number { get; set; }

            [Column("test_second_entity_id")]
            public long SecondId { get; set; }

            [Column("test_third_entity_id")]
            public long ThirdId { get; set; }
        }

        [Table("test_second_entity")] 
        private class TestSecondEntity : EntityBase
        {
            [Column("name")]
            public string? Name { get; set; }

            [Column("age")]
            public int Age { get; set; }
        }

        [Table("test_third_entity")]
        private class TestThirdEntity : EntityBase
        {
            [Column("code")]
            public string? Code3 { get; set; }

            [Column("name")]
            public string? Name3 { get; set; }

            [Column("description")]
            public string? Description3 { get; set; }
        }
    }
}
