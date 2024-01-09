using MongoDB.Driver;

namespace CG4.Impl.MongoDB
{
    /// <summary>
    /// MongoDB collation settings for find and sorting
    /// </summary>
    public interface IMongoCollationSettings
    {
        /// <summary>
        /// Get locale
        /// </summary>
        string Locale { get; }

        /// <summary>
        /// Prioritizes the comparison properties.
        /// </summary>
        CollationStrength CollationStrength { get; }
    }
}
