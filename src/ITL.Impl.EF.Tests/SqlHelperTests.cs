using System.Collections.Generic;
using System.Linq;
using ITL.DataAccess.Helpers;
using Xunit;

namespace ITL.Impl.EF.Tests
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
        public void GetSqlParameter_DictionaryType_WasReturnedArray()
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
