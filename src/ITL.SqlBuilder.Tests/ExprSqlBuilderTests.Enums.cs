using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITL.Impl.Dapper.Poco.ExprOptions;
using Xunit;

namespace ITL.SqlBuilder.Tests
{
    public partial class ExprSqlBuilderTests
    {
        [Fact]
        public void GetAll_ShortTypeEnumWhere_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            ShortTypeEnum val = ShortTypeEnum.Two;

            var sql = builder.GetAll<TestEnumEntity>(
                x => x.As("tee").Where(p => p.ShortTypeEnum == val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE tee.\"short_type_enum\" = 2", sql);
        }

        [Fact]
        public void GetAll_IntTypeEnumWhere_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            IntTypeEnum val = IntTypeEnum.Second;

            var sql = builder.GetAll<TestEnumEntity>(
                x => x.As("tee").Where(p => p.IntTypeEnum == val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE tee.\"int_type_enum\" = 1", sql);
        }

        [Fact]
        public void GetAll_LongTypeEnumWhere_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            LongTypeEnum val = LongTypeEnum.Many;

            var sql = builder.GetAll<TestEnumEntity>(
                x => x.As("tee").Where(p => p.LongTypeEnum == val));

            Assert.NotNull(sql);
            Assert.Contains($"WHERE tee.\"long_type_enum\" = {0xAFFFFFFFF}", sql);
        }

        [Fact]
        public void GetAll_ShortTypeEnumNullableWhere_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            ShortTypeEnum? val = ShortTypeEnum.One;

            var sql = builder.GetAll<TestEnumEntity>(
                x => x.As("tee").Where(p => p.ShortTypeEnumNullable == val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE tee.\"short_type_enum_nullable\" = 1", sql);
        }

        [Fact]
        public void GetAll_IntTypeEnumNullableWhere_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            IntTypeEnum? val = IntTypeEnum.First;

            var sql = builder.GetAll<TestEnumEntity>(
                x => x.As("tee").Where(p => p.IntTypeEnumNullable == val));

            Assert.NotNull(sql);
            Assert.Contains("WHERE tee.\"int_type_enum_nullable\" = 0", sql);
        }

        [Fact]
        public void GetAll_LongTypeEnumNullableWhere_ReturnSql()
        {
            var builder = new ExprSqlBuilder(_sqlSettings);
            LongTypeEnum? val = LongTypeEnum.Many;

            var sql = builder.GetAll<TestEnumEntity>(
                x => x.As("tee").Where(p => p.LongTypeEnumNullable == val));

            Assert.NotNull(sql);
            Assert.Contains($"WHERE tee.\"long_type_enum_nullable\" = {0xAFFFFFFFF}", sql);
        }

        [Table("test_entity")]
        private class TestEnumEntity
        {
            [Column("short_type_enum")]
            public ShortTypeEnum ShortTypeEnum { get; set; }

            [Column("int_type_enum")]
            public IntTypeEnum IntTypeEnum { get; set; }

            [Column("long_type_enum")]
            public LongTypeEnum LongTypeEnum { get; set; }

            [Column("short_type_enum_nullable")]
            public ShortTypeEnum? ShortTypeEnumNullable { get; set; }

            [Column("int_type_enum_nullable")]
            public IntTypeEnum? IntTypeEnumNullable { get; set; }

            [Column("long_type_enum_nullable")]
            public LongTypeEnum? LongTypeEnumNullable { get; set; }
        }

        private enum ShortTypeEnum : short
        {
            Zero = 0,
            One = 1,
            Two = 2,
        }

        private enum IntTypeEnum
        {
            First = 0,
            Second = 1,
        }

        private enum LongTypeEnum : long
        {
            Zero = 0,
            Many = 0xAFFFFFFFF,
            Err = 0xDEADBEEF
        }
    }
}
