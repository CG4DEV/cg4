namespace CG4.Impl.ElasticSearch
{
    /// <summary>
    /// Elasticsearch connection settings
    /// </summary>
    public interface IElasticsearchConnectionSettings
    {
        /// <summary>
        /// Elastic search nodes
        /// </summary>
        IEnumerable<string> Nodes { get; set; }
    }
}
