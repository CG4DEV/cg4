using System.Collections.Concurrent;

namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// Central hub for managing POCO (Plain Old CLR Object) to database mappings.
    /// Provides caching and access to class mapping information for entity types.
    /// </summary>
    public static class PocoHub
    {
        /// <summary>
        /// Thread-safe cache of class mappings indexed by entity type.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, ClassMap> _classMaps = new();

        /// <summary>
        /// Gets the class mapping for a given entity type.
        /// If the mapping doesn't exist in the cache, it creates and caches a new mapping.
        /// </summary>
        /// <param name="entityType">The type of entity to get mapping for.</param>
        /// <returns>A class mapping containing property and table information for the entity type.</returns>
        public static ClassMap GetMap(Type entityType)
        {
            if (!_classMaps.TryGetValue(entityType, out var map))
            {
                map = ClassMap.Map(entityType);

                _classMaps[entityType] = map;
            }

            return map;
        }

        /// <summary>
        /// Gets the class mapping for a given entity type using generic type parameter.
        /// If the mapping doesn't exist in the cache, it creates and caches a new mapping.
        /// </summary>
        /// <typeparam name="T">The type of entity to get mapping for.</typeparam>
        /// <returns>A class mapping containing property and table information for the entity type.</returns>
        public static ClassMap GetMap<T>()
            where T : class
        {
            return GetMap(typeof(T));
        }
    }
}
