using MongoDB.Driver;

namespace ITL.Impl.MongoDB
{
    /// <summary>
    /// Client for connection to MongoDB
    /// </summary>
    public interface IMongoDBClient
    {
        /// <summary>
        /// Returns <see cref="MongoDB.Driver.IMongoDatabase"> connection
        /// </summary>
        IMongoDatabase GetDatabase();
    }
}
