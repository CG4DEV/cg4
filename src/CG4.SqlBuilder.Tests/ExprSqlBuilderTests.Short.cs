using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CG4.Impl.Dapper.Poco.ExprOptions;
using Xunit;

namespace CG4.SqlBuilder.Tests
{
    public partial class ExprSqlBuilderTests
    {
        [Fact]
        public void GetAll_WithShortTypeWhere_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            short val = 13;

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => p.Level == val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"level\" = 13", sql);
        }

        [Fact]
        public void GetAll_WithShortNullableTypeWhere_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            short? val = 13;

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => p.Level == val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"level\" = 13", sql);
        }

        [Fact]
        public void GetAll_WithShortEnumWhere_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var val = ShortType.One;

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => p.ShortTypeLevel == val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"short_type_level\" = 1", sql);
        }

        [Fact]
        public void GetAll_WithShortGreaterThan_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            short val = 13;

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => p.Level > val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"level\" > 13", sql);
        }

        [Fact]
        public void GetAll_WithShortLessThan_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            short val = 13;

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => p.Level < val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"level\" < 13", sql);
        }

        [Fact]
        public void GetAll_WithShortGreaterOrEqual_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            short val = 13;

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => p.Level >= val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"level\" >= 13", sql);
        }

        [Fact]
        public void GetAll_WithShortLessOrEqual_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            short val = 13;

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => p.Level <= val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"level\" <= 13", sql);
        }

        [Fact]
        public void GetAll_WithShortNotEqual_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            short val = 13;

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => p.Level != val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"level\" != 13", sql);
        }

        [Fact]
        public void GetAll_WithShortNullableIsNull_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            short? val = null;

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => p.LevelNullable == val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"level_n\" IS NULL", sql);
        }

        [Fact]
        public void GetAll_WithShortNullableIsNotNull_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            short? val = null;

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => p.LevelNullable != val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"level_n\" IS NOT NULL", sql);
        }

        [Fact]
        public void GetAll_WithShortArrayContains_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var values = new short[] { 1, 2, 3 };

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => values.Contains(p.Level)));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"level\" = ANY (ARRAY [1, 2, 3])", sql);
        }

        [Fact]
        public void GetAll_WithShortComplexCondition_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            short minValue = 10;
            short maxValue = 20;

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => p.Level >= minValue && p.Level <= maxValue));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"level\" >= 10 AND ste.\"level\" <= 20", sql);
        }

        [Fact]
        public void GetAll_WithShortOrCondition_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var type1 = ShortType.One;
            var type2 = ShortType.Two;

            var sql = builder.GetAll<ShortTypeEntity>(
                x => x.As("ste").Where(p => p.ShortTypeLevel == type1 || p.ShortTypeLevel == type2));

            Assert.NotNull(sql);
            Assert.Contains("WHERE ste.\"short_type_level\" = 1 OR ste.\"short_type_level\" = 2", sql);
        }

        [Table("test_entity")]
        private class ShortTypeEntity
        {

            [Column("level")]
            public short Level { get; set; }

            [Column("level_n")]
            public short? LevelNullable { get; set; }

            [Column("short_type_level")]
            public ShortType ShortTypeLevel { get; set; }
        }

        private enum ShortType : short
        {
            Zero,
            One,
            Two,
        }

        [Table("multi_type_entity")]
        private class MultiTypeEntity
        {
            [Column("int_value")]
            public int IntValue { get; set; }

            [Column("long_value")]
            public long LongValue { get; set; }

            [Column("decimal_value")]
            public decimal DecimalValue { get; set; }

            [Column("date_value")]
            public DateTime DateValue { get; set; }

            [Column("guid_value")]
            public Guid GuidValue { get; set; }

            [Column("bool_value")]
            public bool BoolValue { get; set; }

            [Column("string_value")]
            public string? StringValue { get; set; }

            [Column("nullable_int")]
            public int? NullableInt { get; set; }

            [Column("nullable_date")]
            public DateTime? NullableDate { get; set; }
        }

        [Fact]
        public void GetAll_WithIntegerCondition_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            int value = 100;

            var sql = builder.GetAll<MultiTypeEntity>(
                x => x.As("mte").Where(p => p.IntValue > value));

            Assert.NotNull(sql);
            Assert.Contains("WHERE mte.\"int_value\" > 100", sql);
        }

        [Fact]
        public void GetAll_WithLongCondition_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            long value = 1000000L;

            var sql = builder.GetAll<MultiTypeEntity>(
                x => x.As("mte").Where(p => p.LongValue <= value));

            Assert.NotNull(sql);
            Assert.Contains("WHERE mte.\"long_value\" <= 1000000", sql);
        }        [Fact]
        public void GetAll_WithDecimalCondition_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            decimal value = 123.45m;

            var sql = builder.GetAll<MultiTypeEntity>(
                x => x.As("mte").Where(p => p.DecimalValue >= value));

            Assert.NotNull(sql);
            Assert.Contains("WHERE mte.\"decimal_value\" >= 123.4500", sql);
        }[Fact]
        public void GetAll_WithDateTimeCondition_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var date = new DateTime(2025, 5, 21);

            var sql = builder.GetAll<MultiTypeEntity>(
                x => x.As("mte").Where(p => p.DateValue >= date));

            Assert.NotNull(sql);
            Assert.Contains("WHERE mte.\"date_value\" >= '2025-05-21'", sql);
        }

        [Fact]
        public void GetAll_WithGuidCondition_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var guid = Guid.NewGuid();

            var sql = builder.GetAll<MultiTypeEntity>(
                x => x.As("mte").Where(p => p.GuidValue == guid));

            Assert.NotNull(sql);
            Assert.Contains($"WHERE mte.\"guid_value\" = '{guid}'", sql);
        }        [Fact]
        public void GetAll_WithBooleanCondition_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);

            var sql = builder.GetAll<MultiTypeEntity>(
                x => x.As("mte").Where(p => p.BoolValue == true));

            Assert.NotNull(sql);
            Assert.Contains("WHERE mte.\"bool_value\" = TRUE", sql);
        }        [Fact]
        public void GetAll_WithStringLikeCondition_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var searchTerm = "test";

            var sql = builder.GetAll<MultiTypeEntity>(
                x => x.As("mte").Where(p => p.StringValue!.Contains(searchTerm)));

            Assert.NotNull(sql);
            Assert.Contains("WHERE mte.\"string_value\" ILIKE '%test%'", sql);
        }

        [Fact]
        public void GetAll_WithMultipleTypeConditions_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            int intValue = 50;
            var date = DateTime.Today;

            var sql = builder.GetAll<MultiTypeEntity>(
                x => x.As("mte").Where(p => p.IntValue > intValue && p.DateValue == date));

            Assert.NotNull(sql);
            Assert.Contains($"WHERE mte.\"int_value\" > 50 AND mte.\"date_value\" = '{date:yyyy-MM-dd}'", sql);
        }

        [Fact]
        public void GetAll_WithNullableTypeCondition_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            int? nullableValue = null;

            var sql = builder.GetAll<MultiTypeEntity>(
                x => x.As("mte").Where(p => p.NullableInt == nullableValue));

            Assert.NotNull(sql);
            Assert.Contains("WHERE mte.\"nullable_int\" IS NULL", sql);
        }

        [Fact]
        public void GetAll_WithDateTimeRangeCondition_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var startDate = new DateTime(2025, 1, 1);
            var endDate = new DateTime(2025, 12, 31);

            var sql = builder.GetAll<MultiTypeEntity>(
                x => x.As("mte").Where(p => p.DateValue >= startDate && p.DateValue <= endDate));

            Assert.NotNull(sql);
            Assert.Contains("WHERE mte.\"date_value\" >= '2025-01-01' AND mte.\"date_value\" <= '2025-12-31'", sql);
        }

        [Fact]
        public void GetAll_WithArrayConditionMultipleTypes_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var intValues = new[] { 1, 2, 3 };
            var stringValue = "test";

            var sql = builder.GetAll<MultiTypeEntity>(
                x => x.As("mte").Where(p => intValues.Contains(p.IntValue) && p.StringValue == stringValue));

            Assert.NotNull(sql);
            Assert.Contains("WHERE mte.\"int_value\" = ANY (ARRAY [1, 2, 3]) AND mte.\"string_value\" = 'test'", sql);
        }
    }
}
