using System.Collections.Generic;
using System.Linq;
using CG4.Impl.EF.Helpers;
using Xunit;

namespace CG4.Impl.EF.Tests
{
    public class SqlHelperTests
    {
        [Fact]
        public void GetSqlParameter_AnonymousType_WasReturnedArray()
        {
            var result = SqlHelper.GetSqlParameter(new { Id = 1 });
            Assert.Single(result);
            Assert.Equal("Id", result.First().ParameterName);
            Assert.Equal(1, result.First().Value);
        }

        [Fact]
        public void GetSqlParameter_DictionatyType_WasReturnedArray()
        {
            var result = SqlHelper.GetSqlParameter(new Dictionary<string, object>() { { "Id", 1 } });
            Assert.Single(result);
            Assert.Equal("Id", result.First().ParameterName);
            Assert.Equal(1, result.First().Value);
        }

        [Fact]
        public void GetSqlParameter_Nullable_WasReturnedArray()
        {
            var result = SqlHelper.GetSqlParameter(null);
            Assert.Empty(result);
        }
    }
}