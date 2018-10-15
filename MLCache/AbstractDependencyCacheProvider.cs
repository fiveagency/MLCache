using System;
using System.Collections.Generic;

namespace MLCache
{
    /// <summary>
    /// Abstract cache provider with dependency support.
    /// Uses cache client interface to communicate to the client.
    /// Supports cache interoperability.
    /// </summary>
    public abstract class AbstractDependencyCacheProvider : IChainableCacheProvider
    {
        protected abstract ICacheClient Client { get; }

        public void Insert(string key, object value, KeyCacheDependency dependencies)
        {
            PerformInsert(key, value, dependencies, null);
        }
        
        public void Insert(string key, object value, KeyCacheDependency dependencies,
            DateTime absoluteExpiration)
        {
            PerformInsert(key, value, dependencies, absoluteExpiration);
        }

        public void Remove(string key)
        {
            Client.Remove(key);
            InvalidateDependencies(key);
        }

        public void Clear()
        {
            Client.Clear();
        }

        public object GetValue(string key)
        {
            var entry = GetEntry(key);
            return entry != null ? entry.Value : null;
        }

        public CacheEntry GetEntry(string key)
        {
            try
            {
                return Client.Get(key) as CacheEntry;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// A method that actually inserts the cache entry.
        /// </summary>
        private void PerformInsert(string key, object value, KeyCacheDependency dependencies,
            DateTime? absoluteExpiration)
        {
            InvalidateDependencies(key);

            var entry = new CacheEntry(value, dependencies, absoluteExpiration);
            Client.Insert(key, entry, absoluteExpiration);

            UpdateDependencies(key, dependencies);
        }

        /// <summary>
        /// Updates dependencies for a specified key. Adding a dependency
        /// to a key will remove this key from the cache every time any of
        /// the dependant keys is updated or removed from cache.
        /// </summary>
        private void UpdateDependencies(string key, KeyCacheDependency dependencies)
        {
            if (dependencies == null)
            {
                return;
            }

            foreach (var dependency in dependencies)
            {
                var dependenciesKey = CreateDependenciesKey(dependency);
                var dependants = Client.Get(dependenciesKey) as HashSet<string>;
                if (dependants == null)
                {
                    dependants = new HashSet<string>();
                }

                // Add the parent key to dependencies of the dependant
                // key so we can efficiently find keys that need to be
                // invalidated when updating cache entries.
                dependants.Add(key);
                Client.Insert(dependenciesKey, dependants, null);
            }
        }

        /// <summary>
        /// Removes all key dependencies for a specified key.
        /// </summary>
        private void InvalidateDependencies(string key)
        {
            var dependenciesKey = CreateDependenciesKey(key);
            var dependencies = Client.Get(dependenciesKey) as HashSet<string>;
            if (dependencies != null)
            {
                Client.Remove(dependenciesKey);
                foreach (var dependency in dependencies)
                {
                    Remove(dependency);
                }
            }
        }

        /// <summary>
        /// This cache provider creates new cache entries that map 
        /// a list of dependencies for every parent key. This method 
        /// returns a name of this key for a specified parent key.
        /// </summary>
        private static string CreateDependenciesKey(string parentKey)
        {
            return parentKey + "_dependencies";
        }

    }
}
