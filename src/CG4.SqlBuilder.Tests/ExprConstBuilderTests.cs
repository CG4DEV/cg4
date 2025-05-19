﻿using CG4.DataAccess.Domain;
using CG4.DataAccess.Poco;
using CG4.DataAccess.Poco.Expressions;
using CG4.DataAccess.Poco.Visitors;
using CG4.Impl.Dapper.Poco.ExprOptions;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit;

namespace CG4.SqlBuilder.Tests
{
    public class ExprConstBuilderTests
    {
        private readonly ISqlSettings _sqlSettings = new PostgreSqlOptions();

        [Fact]
        public void GetAll_QueryByNewExprType_BuildSQLWithNewConstType()
        {
            ExprConstBuildRegister.Register(typeof(DateOnly), new DateOnlyExprConstBuilder());

            var builder = new ExprSqlBuilder(_sqlSettings);
            var date = new DateOnly(2023, 11, 22);
            var expr = ExprHelper.GenerateWhere<TestEntity>(a1 => a1.Date == date);

            var sql = builder.GetAll<TestEntity>(x => x.AppendWhere(expr));

            Assert.NotNull(sql);
            Assert.Contains(@"WHERE a1.""date"" = 20231122", sql);
        }

        [Table("test_entity")]
        private class TestEntity : EntityBase
        {
            [Column("code")]
            public string Code { get; set; }

            [Column("number")]
            public int Number { get; set; }

            [Column("test_second_entity_id")]
            public long SecondId { get; set; }

            [Column("date")]
            public DateOnly Date { get; set; }
        }

        private class DateOnlyExprConstBuilder : IExprConstBuilder
        {
            public ExprConst Build(Type type, object value)
            {
                return new ExprDateOnly { Date = (DateOnly)value };
            }
        }

        private class ExprDateOnly : ExprConst
        {
            public DateOnly Date { get; set; }

            public override void Accept(IExprVisitor visitor)
            {
                visitor.VisitConstValue($"{Date.Year:0000}{Date.Month:00}{Date.Day:00}");
            }
        }
    }
}
