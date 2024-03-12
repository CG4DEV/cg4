using System.ComponentModel.DataAnnotations.Schema;
using BenchmarkDotNet.Attributes;
using ITL.DataAccess.Domain;
using ITL.DataAccess.Poco;
using ITL.Impl.Dapper.Poco.ExprOptions;

namespace ITL.SqlBuilder.Benchmark.Tests
{
    [MemoryDiagnoser]
    public class SqlBuilderTests
    {
        private readonly ExprSqlBuilder _builder;

        public SqlBuilderTests()
        {
            var sqlSettings = new PostreSqlOptions();
            _builder = new ExprSqlBuilder(sqlSettings);
        }

        [Benchmark]
        public string Delete() => _builder.DeleteById<TestEntity>();

        [Benchmark]
        public string Insert() => _builder.Insert<TestEntity>();

        [Benchmark]
        public string UpdateById() => _builder.UpdateById<TestEntity>();

        [Benchmark]
        public string GetAll() => _builder.GetAll<TestEntity>();

        [Benchmark]
        public string GetById() => _builder.GetById<TestEntity>();
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
    }

}
