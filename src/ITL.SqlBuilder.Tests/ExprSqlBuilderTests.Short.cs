using System.ComponentModel.DataAnnotations.Schema;
using ITL.Impl.Dapper.Poco.ExprOptions;
using Xunit;

namespace ITL.SqlBuilder.Tests
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
    }
}
