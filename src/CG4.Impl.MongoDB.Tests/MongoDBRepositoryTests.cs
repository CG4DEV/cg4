using System;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace CG4.Impl.MongoDB.Tests
{
    public class MongoDBRepositoryTests
    {
        private readonly Mock<IMongoDBClient> _clientMock;
        private readonly Mock<IMongoCollationSettings> _settingsMock;
        private MongoDBRepositoryOnlyForTest _repository;

        public MongoDBRepositoryTests()
        {
            _clientMock = new Mock<IMongoDBClient>();
            _settingsMock = new Mock<IMongoCollationSettings>();           
        }

        [Theory]
        [InlineData(typeof(String), "String")]
        [InlineData(typeof(GenericType<String>), "GenericType_String")]
        [InlineData(typeof(GenericType<GenericType<String>>), "GenericType_GenericType_String")]
        [InlineData(typeof(GenericType<String, Int32>), "GenericType_String_Int32")]
        [InlineData(typeof(GenericType<GenericType<String, Int32>, GenericType<String, Int32>>), "GenericType_GenericType_String_Int32_GenericType_String_Int32")]
        public void GetCollectionName_FromGenericType_ReturnsCollectionName(Type type, string expected)
        {
            _repository = new MongoDBRepositoryOnlyForTest(_clientMock.Object);

            var collectionName = _repository.GetCollectionName(type);
            Assert.Equal(expected, collectionName);
        }

        [Fact]
        public void GetCollation_ExistCollationSettings_ReturnsCollation()
        {
            var expected = new { Locale = "en", CollationStrength = CollationStrength.Secondary };

            _settingsMock.SetupGet(x => x.Locale).Returns(expected.Locale);
            _settingsMock.SetupGet(x => x.CollationStrength).Returns(expected.CollationStrength);

            _repository = new MongoDBRepositoryOnlyForTest(_clientMock.Object, _settingsMock.Object);

            var collation = _repository.GetCollation();

            Assert.Equal(expected.Locale, collation.Locale);
            Assert.Equal(expected.CollationStrength, collation.Strength);
        }

        [Fact]
        public void GetCollation_NotExistCollationSettings_ReturnsNull()
        {
            _repository = new MongoDBRepositoryOnlyForTest(_clientMock.Object);

            var collation = _repository.GetCollation();

            Assert.Null(collation);
        }

        private class GenericType<T>
        {
            public T Value { get; set; }
        }

        private class GenericType<T1, T2>
        {
            public T1 Value1 { get; set; }

            public T2 Value2 { get; set; }
        }

        private class MongoDBRepositoryOnlyForTest : MongoDBRepository
        {
            public MongoDBRepositoryOnlyForTest(IMongoDBClient client) : base(client)
            {
            }

            public MongoDBRepositoryOnlyForTest(IMongoDBClient client, IMongoCollationSettings settings) : base(client, settings)
            {
            }

            public string GetCollectionName(Type type)
            {
                return base.GetCollectionName(type);
            }

            public Collation GetCollation()
            {
                return base.GetCollation();
            }
        }
    }
}
