namespace MLCacheExamples
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using MLCache;
    using MLCache.Memcached;
    using MLCache.Memory;

    public class Program
    {
        private static readonly UserRepository Repository = new UserRepository();

        public static void Main(string[] args)
        {
            MemoryCacheExample();
            MultiLayerCacheExample();

            Console.WriteLine("Done");
        }

        private static IEnumerable<User> SearchUsers(string query, ICacheProvider cache)
        {
            var cacheKey = string.Format("query:{0}", query);
            var value = cache.GetValue(cacheKey) as IEnumerable<User>;
            if (value != null)
            {
                Console.WriteLine("Result fetched from cache");
                return value;
            }

            Console.WriteLine("Result fetched from repository and stored to cache");
            value = Repository.Search(query);
            cache.Insert(cacheKey, value, null);
            return Repository.Search(query);
        }

        private static void MemoryCacheExample()
        {
            Console.WriteLine("---Memory cache test---");
            // setup cache
            var cache = new MemoryCacheProvider(1);
            var users = SearchUsers("mirko", cache);
            users = SearchUsers("mirko", cache);
            Thread.Sleep(2000);
            users = SearchUsers("mirko", cache);
        }

        private static void MultiLayerCacheExample()
        {
            Console.WriteLine("---Multilayer cache test---");
            // setup cache
            var memoryCache = new MemoryCacheProvider(2);
            var memcachedCache = new MemcachedCacheProvider("127.0.0.1", 11211);
            var cache = new MultiLevelCacheProvider(new IChainableCacheProvider[] { memoryCache, memcachedCache });
            var users = SearchUsers("mirko", cache);
            users = SearchUsers("mirko", cache);
            Thread.Sleep(2000);
            users = SearchUsers("mirko", cache);
        }
    }
}
