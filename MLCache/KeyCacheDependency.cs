using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace MLCache
{
    [Serializable]
    public class KeyCacheDependency : IEnumerable<string>
    {
        private readonly string[] cacheKeys;

        public string[] CacheKeys { get { return cacheKeys; } }

        public KeyCacheDependency(string cacheKey)
        {
            cacheKeys = new [] { cacheKey };
        }

        public KeyCacheDependency(IEnumerable<string> cacheKeys)
        {
            this.cacheKeys = cacheKeys.ToArray();
        }

        public KeyCacheDependency(string[] cacheKeys)
        {
            this.cacheKeys = cacheKeys;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return ((IEnumerable<string>) cacheKeys).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
