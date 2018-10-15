using System;
using System.Collections.Generic;
using System.Linq;

namespace MLCache
{
    /// <summary>
    /// Multi level cache is a composite cache where each level 
    /// represents one chainable cache.
    /// Caches should be sorted by latency where at top (first)
    /// should be cache with lowest latency, and at bottom (last)
    /// should be cache with highest latency. Multi level cache
    /// will always try to get value from top cache. If it misses
    /// it will try to get value from next in hierarchy until value
    /// is found or levels are exhausted. If value is found, before
    /// returning value, value will be propagated to all higher level
    /// caches.
    /// Upon insertion all levels will receive value.
    /// </summary>
    public class MultiLevelCacheProvider : ICacheProvider
    {
        private readonly List<IChainableCacheProvider> cacheProviders;

        public MultiLevelCacheProvider(IEnumerable<IChainableCacheProvider> cacheProviders)
        {
            this.cacheProviders = cacheProviders.ToList();
        }

        public void Insert(string key, object value, KeyCacheDependency dependencies)
        {
            foreach (var cacheProvider in cacheProviders)
            {
                cacheProvider.Insert(key, value, dependencies);
            }
        }

        public void Insert(string key, object value, KeyCacheDependency dependencies, DateTime absoluteExpiration)
        {
            foreach (var cacheProvider in cacheProviders)
            {
                cacheProvider.Insert(key, value, dependencies, absoluteExpiration);
            }
        }

        public void Remove(string key)
        {
            foreach (var cacheProvider in cacheProviders)
            {
                cacheProvider.Remove(key);
            }
        }

        public object GetValue(string key)
        {
            foreach (var cacheProvider in cacheProviders)
            {
                var entry = cacheProvider.GetEntry(key);
                if (entry == null)
                {
                    continue;
                }
                UpdateHigherLevelCacheProviders(cacheProvider, key, entry);
                return entry.Value;
            }
            return null;
        }

        private void UpdateHigherLevelCacheProviders(ICacheProvider sourceCacheProvider, string key, CacheEntry entry)
        {
            foreach (var cacheProvider in cacheProviders.TakeWhile(cacheProvider => cacheProvider != sourceCacheProvider))
            {
                if (entry.AbsoluteExpiration.HasValue)
                {
                    cacheProvider.Insert(key, entry.Value, entry.Dependencies, entry.AbsoluteExpiration.Value);
                }
                else
                {
                    cacheProvider.Insert(key, entry.Value, entry.Dependencies);
                }
            }
        }

        public void Clear()
        {
            foreach (var cacheProvider in cacheProviders)
            {
                cacheProvider.Clear();
            }
        }

    }
}
