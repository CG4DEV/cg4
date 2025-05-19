using System;
using System.ComponentModel.DataAnnotations.Schema;
using CG4.DataAccess.Domain;
using CG4.DataAccess.Poco;
using CG4.DataAccess.Poco.Expressions;
using CG4.Impl.Dapper.Poco.ExprOptions;
using Xunit;

namespace CG4.SqlBuilder.Tests
{
    public partial class ExprSqlBuilderTests
    {
        private readonly ISqlSettings _sqlSettings = new PostgreSqlOptions();

        [Fact]
        public void DeleteById_CorrectArguments_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.DeleteById<TestEntity>();

            Assert.NotNull(sql);
            Assert.Equal("DELETE FROM \"test_entity\" WHERE \"id\" = @Id", sql);
        }

        [Fact]
        public void Insert_CorrectArguments_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.Insert<TestEntity>();

            Assert.NotNull(sql);
            Assert.Equal("INSERT INTO \"test_entity\" (\"code\", \"number\", \"test_second_entity_id\", \"guidcode\", \"create_date\", \"update_date\") VALUES (@Code, @Number, @SecondId, @UserId, @CreateDate, @UpdateDate) RETURNING id", sql);
        }

        [Fact]
        public void Insert_NotMappedAttribute_ReturnSqlWithoutAttribute()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.Insert<TestSecondEntity>();

            Assert.NotNull(sql);
            Assert.Equal("INSERT INTO \"test_second_entity\" (\"name\", \"create_date\", \"update_date\") VALUES (@Name, @CreateDate, @UpdateDate) RETURNING id", sql);
        }

        [Fact]
        public void UpdateById_CorrectArguments_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.UpdateById<TestEntity>();

            Assert.NotNull(sql);
            Assert.Equal("UPDATE \"test_entity\" SET \"code\" = @Code, \"number\" = @Number, \"test_second_entity_id\" = @SecondId, \"guidcode\" = @UserId, \"update_date\" = @UpdateDate WHERE \"id\" = @Id", sql);
        }

        [Fact]
        public void UpdateById_NotMappedAttribute_ReturnSqlWithoutAttribute()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.UpdateById<TestSecondEntity>();

            Assert.NotNull(sql);
            Assert.Equal("UPDATE \"test_second_entity\" SET \"name\" = @Name, \"update_date\" = @UpdateDate WHERE \"id\" = @Id", sql);
        }

        [Fact]
        public void GetById_CorrectArguments_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetById<TestEntity>(x => x.Where(p => p.Number == 10));

            Assert.NotNull(sql);
            Assert.Equal(@"SELECT t.""code"" AS ""Code"", t.""number"" AS ""Number"", t.""test_second_entity_id"" AS ""SecondId"", t.""guidcode"" AS ""UserId"", t.""id"" AS ""Id"", t.""create_date"" AS ""CreateDate"", t.""update_date"" AS ""UpdateDate""
FROM ""test_entity"" AS t
WHERE t.""number"" = 10 AND t.""id"" = @Id", sql.TrimEnd());
        }

        [Fact]
        public void GetById_NotMappedAttribute_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetById<TestSecondEntity>();

            Assert.NotNull(sql);
            Assert.Equal(@"SELECT t.""name"" AS ""Name"", t.""id"" AS ""Id"", t.""create_date"" AS ""CreateDate"", t.""update_date"" AS ""UpdateDate""
FROM ""test_second_entity"" AS t
WHERE t.""id"" = @Id", sql.TrimEnd());
        }

        [Fact]
        public void GetAll_CorrectArguments_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetAll<TestEntity>();

            Assert.NotNull(sql);
            Assert.Equal(@"SELECT t.""code"" AS ""Code"", t.""number"" AS ""Number"", t.""test_second_entity_id"" AS ""SecondId"", t.""guidcode"" AS ""UserId"", t.""id"" AS ""Id"", t.""create_date"" AS ""CreateDate"", t.""update_date"" AS ""UpdateDate""
FROM ""test_entity"" AS t", sql.TrimEnd());
        }

        [Fact]
        public void GetAll_NotMapped_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetAll<TestSecondEntity>();

            Assert.NotNull(sql);
            Assert.Equal(@"SELECT t.""name"" AS ""Name"", t.""id"" AS ""Id"", t.""create_date"" AS ""CreateDate"", t.""update_date"" AS ""UpdateDate""
FROM ""test_second_entity"" AS t", sql.TrimEnd());
        }

        [Fact]
        public void GetAll_CorrectArguments_ReturnPagedSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetAll<TestEntity>(x => x.Limit(10).Offset(10));

            Assert.NotNull(sql);
            Assert.Equal(@"SELECT t.""code"" AS ""Code"", t.""number"" AS ""Number"", t.""test_second_entity_id"" AS ""SecondId"", t.""guidcode"" AS ""UserId"", t.""id"" AS ""Id"", t.""create_date"" AS ""CreateDate"", t.""update_date"" AS ""UpdateDate""
FROM ""test_entity"" AS t
LIMIT 10 OFFSET 10", sql.TrimEnd());
        }

        [Fact]
        public void GetAll_WithWhereAndOrder_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetAll<TestEntity>(
                x => x.Where(p => p.Code == "test")
                    .OrderBy(p => p.CreateDate)
                    .OrderByDesc(p => p.Number));

            Assert.NotNull(sql);
            Assert.Equal(@"SELECT t.""code"" AS ""Code"", t.""number"" AS ""Number"", t.""test_second_entity_id"" AS ""SecondId"", t.""guidcode"" AS ""UserId"", t.""id"" AS ""Id"", t.""create_date"" AS ""CreateDate"", t.""update_date"" AS ""UpdateDate""
FROM ""test_entity"" AS t
WHERE t.""code"" = 'test'
ORDER BY t.""create_date"" ASC, t.""number"" DESC", sql.TrimEnd());
        }

        [Fact]
        public void GetAll_WithJoins_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetAll<TestEntity>(
                x => x.Where(p => p.Code == "test")
                    .Join<TestSecondEntity, long>(p => p.SecondId, "Second"));

            Assert.NotNull(sql);
            Assert.Equal(@"SELECT t.""code"" AS ""Code"", t.""number"" AS ""Number"", t.""test_second_entity_id"" AS ""SecondId"", t.""guidcode"" AS ""UserId"", t.""id"" AS ""Id"", t.""create_date"" AS ""CreateDate"", t.""update_date"" AS ""UpdateDate"", t0.""name"" AS ""SecondName"", t0.""create_date"" AS ""SecondCreateDate"", t0.""update_date"" AS ""SecondUpdateDate""
FROM ""test_entity"" AS t
INNER JOIN ""test_second_entity"" AS t0 ON t0.""id"" = t.""test_second_entity_id""
WHERE t.""code"" = 'test'", sql.TrimEnd());
        }

        //[Fact]
        //public void GetAll_WithContactJoins_ReturnSql()
        //{
        //    var builder = new ExprSqlBuilder(_sqlSettings);

        //    var sql = builder.GetAll<Contact>(
        //        x => x.Join<Department, long?>(j => j.DepartmentId, "Department")
        //            .Join<Position, long?>(j => j.PositionId, "Position"));

        //    Assert.NotNull(sql);
        //    Assert.Contains("JOIN \"departments\" AS Department ON Department.\"id\" = t.\"department_id\"", sql);
        //    Assert.Contains("JOIN \"positions\" AS Position ON Position.\"id\" = t.\"position_id\"", sql);
        //}

        [Fact]
        public void GetAll_CorrectArguments_ReturnSqlWithWhere()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetAll<TestEntity>(
                x => x.Where(p => p.Code == "my code")
                    .OrderBy(p => p.CreateDate)
                    .Join<TestSecondEntity, long>(j => j.SecondId, "Second")
                    .OrderBy(p => p.CreateDate)
                    .Where(p => p.Name == "my name"));

            Assert.NotNull(sql);
            Assert.Contains("INNER JOIN \"test_second_entity\" AS t0 ON t0.\"id\" = t.\"test_second_entity_id\"", sql);
            Assert.Contains("t.\"code\" = 'my code'", sql);
            Assert.Contains("t0.\"name\" = 'my name'", sql);
            Assert.Contains("ORDER BY t.\"create_date\" ASC, t0.\"create_date\" ASC", sql);
        }

        [Fact]
        public void GetAll_WithNullableIntFilter_ReturnCorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetAll<TestEntityNullableTypes>(
                x => x.Where(p => p.NilInt == 1));

            Assert.NotNull(sql);
            Assert.Contains("t.\"nil_int\" = 1", sql);
        }

        [Fact]
        public void GetAll_WithNullableLongFilter_ReturnCorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetAll<TestEntityNullableTypes>(
                x => x.Where(p => p.NilLong == 1));

            Assert.NotNull(sql);
            Assert.Contains("t.\"nil_long\" = 1", sql);
        }

        [Fact]
        public void GetAll_WithNullableDateTimeOffsetFilter_ReturnCorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var dt = DateTimeOffset.FromUnixTimeMilliseconds(1649755688682);

            var sql = builder.GetAll<TestEntityNullableTypes>(
                x => x.Where(p => p.NilDateTimeOffset == dt));

            Assert.NotNull(sql);
            Assert.Contains("t.\"nil_date_time_offset\" = '2022-04-12 09:28:08.682 +00:00'", sql);
        }

        [Fact]
        public void GetAll_WithNullableBoolFilter_ReturnCorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetAll<TestEntityNullableTypes>(
                x => x.Where(p => p.NilBool == true));

            Assert.NotNull(sql);
            Assert.Contains("t.\"nil_bool\" = TRUE", sql);
        }

        [Fact]
        public void GetAll_TreeOfExpressions_ReturnCorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var idColumn = new ExprColumn("t", "id");
            var codeColumn = new ExprColumn("t", "code");
            var numberColumn = new ExprColumn("t", "number");
            var nameColumn = new ExprColumn("t", "name");

            var expr = idColumn == 12L
                & (codeColumn == "123" | codeColumn == "222")
                & numberColumn != 44
                & nameColumn == "test";

            var sql = builder.GetAll<TestEntity>(x =>
            x.Join<TestSecondEntity, long>(s => s.SecondId, "Second")
            .AppendWhere(expr));

            Assert.NotNull(sql);
            Assert.Contains(@"WHERE t.""id"" = 12 AND ((t.""code"" = '123') OR (t.""code"" = '222')) AND t.""number"" != 44 AND t.""name"" = 'test'", sql);
        }

        [Fact]
        public void GetAll_TreeOfExpressions_SwapAliases()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var idColumn = new ExprColumn("a1", "id");
            var codeColumn = new ExprColumn("a1", "code");
            var numberColumn = new ExprColumn("a1", "number");
            var nameColumn = new ExprColumn("a2", "name");

            var expr = idColumn == 12L
                & (codeColumn == "123" | codeColumn == "222")
                & numberColumn != 44
                & nameColumn == "test";

            var sql = builder.GetAll<TestEntity>(x =>
            x.As("a1")
            .Join<TestSecondEntity, long>(s => s.SecondId, "Second")
            .As("a2")
            .AppendWhere(expr));

            Assert.NotNull(sql);

            Assert.Contains(@"FROM ""test_entity"" AS a1", sql);
            Assert.Contains(@"INNER JOIN ""test_second_entity"" AS a2 ON a2.""id"" = a1.""test_second_entity_id""", sql);
            Assert.Contains(@"WHERE a1.""id"" = 12 AND ((a1.""code"" = '123') OR (a1.""code"" = '222')) AND a1.""number"" != 44 AND a2.""name"" = 'test'", sql);
        }

        [Fact]
        public void GetAll_TreeOfExpressions_GreaterThanOrEq()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var numberColumn = new ExprColumn("a1", "number");

            var expr = numberColumn >= 1;

            var sql = builder.GetAll<TestEntity>(x => x.AppendWhere(expr));

            Assert.NotNull(sql);

            Assert.Contains(@"WHERE a1.""number"" >= 1", sql);
        }

        [Fact]
        public void GetAll_TreeOfExpressions_GreaterThan()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var numberColumn = new ExprColumn("a1", "number");

            var expr = numberColumn > 1;

            var sql = builder.GetAll<TestEntity>(x => x.AppendWhere(expr));

            Assert.NotNull(sql);

            Assert.Contains(@"WHERE a1.""number"" > 1", sql);
        }

        [Fact]
        public void GetAll_TreeOfExpressions_LessThanOrEq()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var numberColumn = new ExprColumn("a1", "number");

            var expr = numberColumn <= 1;

            var sql = builder.GetAll<TestEntity>(x => x.AppendWhere(expr));

            Assert.NotNull(sql);

            Assert.Contains(@"WHERE a1.""number"" <= 1", sql);
        }

        [Fact]
        public void GetAll_TreeOfExpressions_LessThan()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var numberColumn = new ExprColumn("a1", "number");

            var expr = numberColumn < 1;

            var sql = builder.GetAll<TestEntity>(x => x.AppendWhere(expr));

            Assert.NotNull(sql);

            Assert.Contains(@"WHERE a1.""number"" < 1", sql);
        }

        [Fact]
        public void GetAll_TreeOfExpressions_NotWithNull()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var codeExpr = ExprHelper.GenerateWhere<TestEntity>(a1 => a1.Code != null);

            var expr = codeExpr;

            var sql = builder.GetAll<TestEntity>(x => x.AppendWhere(expr));

            Assert.NotNull(sql);

            Assert.Contains(@"WHERE a1.""code"" IS NOT NULL", sql);
        }

        [Fact]
        public void GetAll_TreeOfExpressions_NotWithoutNull()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var codeExpr = ExprHelper.GenerateWhere<TestEntity>(a1 => a1.Code != "test");

            var expr = codeExpr;

            var sql = builder.GetAll<TestEntity>(x => x.AppendWhere(expr));

            Assert.NotNull(sql);

            Assert.Contains(@"WHERE a1.""code"" != 'test'", sql);
        }

        [Fact]
        public void GetAll_TreeOfExpressions_EqualWithNull()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var codeExpr = ExprHelper.GenerateWhere<TestEntity>(a1 => a1.Code == null);

            var expr = codeExpr;

            var sql = builder.GetAll<TestEntity>(x => x.AppendWhere(expr));

            Assert.NotNull(sql);

            Assert.Contains(@"WHERE a1.""code"" IS NULL", sql);
        }

        [Fact]
        public void GetAll_TreeOfExpressions_EqualWithoutNull()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var codeExpr = ExprHelper.GenerateWhere<TestEntity>(a1 => a1.Code == "test");

            var expr = codeExpr;

            var sql = builder.GetAll<TestEntity>(x => x.AppendWhere(expr));

            Assert.NotNull(sql);

            Assert.Contains(@"WHERE a1.""code"" = 'test'", sql);
        }

        [Fact]
        public void GetAll_TreeOfExpressionsByHelper_SwapAliases()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var idCheck = ExprHelper.GenerateWhere<TestEntity>(a1 => a1.Id == 12L);
            var codeIs123 = ExprHelper.GenerateWhere<TestEntity>(a1 => a1.Code == "123");
            var codeIs222 = ExprHelper.GenerateWhere<TestEntity>(a1 => a1.Code == "222");
            var numberIsnt44 = ExprHelper.GenerateWhere<TestEntity>(a1 => a1.Number != 44);
            var namecheck = ExprHelper.GenerateWhere<TestSecondEntity>(a2 => a2.Name == "test");

            var expr = idCheck
                & (codeIs123 | codeIs222)
                & numberIsnt44;

            var sql = builder.GetAll<TestEntity>(x =>
            x.Where(expr)
            .As("a1")
            .Join<TestSecondEntity, long>(s => s.SecondId, "Second")
            .As("a2")
            .Where(namecheck));

            Assert.NotNull(sql);

            Assert.Contains(@"FROM ""test_entity"" AS a1", sql);
            Assert.Contains(@"INNER JOIN ""test_second_entity"" AS a2 ON a2.""id"" = a1.""test_second_entity_id""", sql);
            Assert.Contains(@"WHERE a1.""id"" = 12 AND ((a1.""code"" = '123') OR (a1.""code"" = '222')) AND a1.""number"" != 44 AND a2.""name"" = 'test'", sql);
        }

        [Fact]
        public void GetAll_EmptyExpressions_CorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var empty = ExprHelper.Empty();

            var sql = builder.GetAll<TestEntity>(x =>
            x.Where(empty)
            .As("a1")
            .Join<TestSecondEntity, long>(s => s.SecondId, "Second")
            .As("a2"));

            Assert.NotNull(sql);

            Assert.Contains(@"FROM ""test_entity"" AS a1", sql);
            Assert.Contains(@"INNER JOIN ""test_second_entity"" AS a2 ON a2.""id"" = a1.""test_second_entity_id""", sql);
            Assert.Contains(@"WHERE TRUE", sql);
        }

        [Fact]
        public void GetAll_ExprWithEmptyExpressions_CorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var expr = ExprHelper.Empty();

            expr = expr.And<TestEntity>(x => x.Id == 12L);

            var sql = builder.GetAll<TestEntity>(x =>
            x.Where(expr)
            .As("a1")
            .Join<TestSecondEntity, long>(s => s.SecondId, "Second")
            .As("a2"));

            Assert.NotNull(sql);

            Assert.Contains(@"FROM ""test_entity"" AS a1", sql);
            Assert.Contains(@"INNER JOIN ""test_second_entity"" AS a2 ON a2.""id"" = a1.""test_second_entity_id""", sql);
            Assert.Contains(@"WHERE a1.""id"" = 12", sql);
        }

        [Fact]
        public void GetAll_ExprGuid_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var guid = Guid.NewGuid();


            var sql = builder.GetAll<TestEntity>(x => x.Where(p => p.UserId == guid));

            Assert.NotNull(sql);
            Assert.Equal(
                $@"SELECT t.""code"" AS ""Code"", t.""number"" AS ""Number"", t.""test_second_entity_id"" AS ""SecondId"", t.""guidcode"" AS ""UserId"", t.""id"" AS ""Id"", t.""create_date"" AS ""CreateDate"", t.""update_date"" AS ""UpdateDate""
FROM ""test_entity"" AS t
WHERE t.""guidcode"" = '{guid}'
",
                sql);
        }

        [Table("test_entity")]
        public class TestEntity : EntityBase
        {
            [Column("code")]
            public string Code { get; set; }

            [Column("number")]
            public int Number { get; set; }

            [Column("test_second_entity_id")]
            public long SecondId { get; set; }

            [Column("guidcode")]
            public Guid UserId { get; set; }
        }

        [Table("test_second_entity")]
        public class TestSecondEntity : EntityBase
        {
            [Column("name")]
            public string Name { get; set; }

            [NotMapped]
            public int Age { get; set; }
        }

        [Table("nullable_types")]
        public class TestEntityNullableTypes : EntityBase
        {
            [Column("nil_int")]
            public int? NilInt { get; set; }

            [Column("nil_long")]
            public long? NilLong { get; set; }

            [Column("nil_date_time_offset")]
            public DateTimeOffset? NilDateTimeOffset { get; set; }

            [Column("nil_bool")]
            public bool? NilBool { get; set; }
        }
    }
}
