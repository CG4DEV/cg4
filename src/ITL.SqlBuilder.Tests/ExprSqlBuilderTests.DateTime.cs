using System;
using System.ComponentModel.DataAnnotations.Schema;
using ITL.Impl.Dapper.Poco.ExprOptions;
using ITL.DataAccess.Domain;
using Xunit;

namespace ITL.SqlBuilder.Tests
{
    public partial class ExprSqlBuilderTests
    {
        [Fact]
        public void GetAll_WithDateTimeFilter_ReturnCorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var dt = new DateTime(2022, 4, 12, 9, 28, 8, 682);

            var sql = builder.GetAll<TestDateTimeEntity>(
                x => x.Where(p => p.DateTime == dt));

            Assert.NotNull(sql);
            Assert.Contains("t.\"date_time\" = '2022-04-12 09:28:08.682'", sql);
        }

        [Fact]
        public void GetAll_WithNullableDateTimeFilter_ReturnCorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var dt = new DateTime(2022, 4, 12, 9, 28, 8, 682);

            var sql = builder.GetAll<TestDateTimeEntity>(
                x => x.Where(p => p.NulDateTime == dt));

            Assert.NotNull(sql);
            Assert.Contains("t.\"nul_date_time\" = '2022-04-12 09:28:08.682'", sql);
        }

        [Fact]
        public void GetAll_WithNullDateTimeFilter_ReturnCorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            DateTime? dt = null;

            var sql = builder.GetAll<TestDateTimeEntity>(
                x => x.Where(p => p.NulDateTime == dt));

            Assert.NotNull(sql);
            Assert.Contains("t.\"nul_date_time\" IS NULL", sql);
        }

        [Fact]
        public void GetAll_WithGreaterThanOrEqAndLessThanOrEqDateTimeFilter_ReturnCorrectSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            var firstDt = new DateTime(2022, 4, 12, 9, 28, 8, 682);
            var secondDt = new DateTime(2022, 4, 13, 9, 28, 8, 682);

            var sql = builder.GetAll<TestDateTimeEntity>(
                x => x.Where(p => p.DateTime >= firstDt && p.DateTime <= secondDt));

            Assert.NotNull(sql);
            Assert.Contains("t.\"date_time\" >= '2022-04-12 09:28:08.682' AND t.\"date_time\" <= '2022-04-13 09:28:08.682'", sql);
        }

        [Table("test_date_time_entity")]
        public class TestDateTimeEntity : EntityBase
        {
            [Column("date_time")]
            public DateTime DateTime { get; set; }

            [Column("nul_date_time")]
            public DateTime? NulDateTime { get; set; }
        }
    }
}
