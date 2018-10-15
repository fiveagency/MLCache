using System;
using System.Collections.Generic;

namespace MLCache.Memory
{
    internal class MemoryCacheClient : ICacheClient
    {
        private readonly IDictionary<string, Tuple<object, DateTime?>> cacheStorage = 
            new Dictionary<string, Tuple<object, DateTime?>>();

        private readonly int? maxDurationSeconds;

        public MemoryCacheClient(int? maxDurationSeconds)
        {
            this.maxDurationSeconds = maxDurationSeconds;
        }

        public object Get(string key)
        {
            var now = DateTime.Now;
            Tuple<object, DateTime?> entry;
            if (cacheStorage.TryGetValue(key, out entry))
            {
                if (entry.Item2.HasValue && entry.Item2.Value <= now)
                {
                    cacheStorage.Remove(key);
                    return null;
                }

                return entry.Item1;
            }

            return null;
        }

        public bool Insert(string key, object value, DateTime? absoluteExpiration)
        {
            if (maxDurationSeconds.HasValue)
            {
                var minExpiration = DateTime.Now.AddSeconds(maxDurationSeconds.Value);
                if (!absoluteExpiration.HasValue || minExpiration < absoluteExpiration.Value)
                {
                    absoluteExpiration = minExpiration;
                }
            }
            cacheStorage[key] = new Tuple<object, DateTime?>(value, absoluteExpiration);
            return true;
        }

        public void Remove(string key)
        {
            cacheStorage.Remove(key);
        }

        public void Clear()
        {
            cacheStorage.Clear();
        }
    }
}
