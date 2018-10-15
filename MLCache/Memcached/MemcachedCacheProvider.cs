namespace MLCache.Memcached
{
    using System.Net;

    /// <summary>
    /// Provider for memcached cache.
    /// </summary>
    public class MemcachedCacheProvider : AbstractDependencyCacheProvider
    {
        private readonly MemcachedClient client;

        public MemcachedCacheProvider(string serverAddress, int serverPort)
        {
            this.client = new MemcachedClient(IPAddress.Parse(serverAddress), serverPort);
        }

        protected override ICacheClient Client { get { return client; } }
        
    }
}
