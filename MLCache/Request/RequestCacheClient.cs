using System;
using System.Collections.Generic;
using System.Web;

namespace MLCache.Request
{
    internal class RequestCacheClient : ICacheClient
    {
        private const string CacheName = "RequestCache";

        private static IDictionary<string, object> Cache
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return null;
                }

                var cache = HttpContext.Current.Items[CacheName] as IDictionary<string, object>;
                if (cache != null)
                {
                    return cache;
                }

                cache = new Dictionary<string, object>();
                HttpContext.Current.Items[CacheName] = cache;
                return cache;
            }
        }

        public object Get(string key)
        {
            if (Cache == null)
            {
                return null;
            }

            object value;
            Cache.TryGetValue(key, out value);
            return value;
        }

        public bool Insert(string key, object value, DateTime? absoluteExpiration)
        {
            if (Cache == null)
            {
                return false;
            }

            Cache[key] = value;
            return true;
        }

        public void Remove(string key)
        {
            if (Cache != null)
            {
                Cache.Remove(key);
            }
        }

        public void Clear()
        {
            if (Cache != null)
            {
                Cache.Clear();
            }
        }
    }
}
