using MongoDB.Driver;

namespace CG4.Impl.MongoDB
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
