using Nest;

namespace ITL.Impl.ElasticSearch
{
    /// <summary>
    /// Elasticsearch client factory
    /// </summary>
    public interface IElasticsearchConnectionFactory
    {
        /// <summary>
        /// Create new client instance
        /// </summary>
        IElasticClient CreateClient();

        /// <summary>
        /// Create new client instance
        /// </summary>
        Task<IElasticClient> CreateClientAsync();
    }
}
