namespace MLCache.Request
{
    /// <summary>
    /// Provider for request cache.
    /// </summary>
    public class RequestCacheProvider : AbstractDependencyCacheProvider
    {
        private readonly RequestCacheClient client;

        public RequestCacheProvider()
        {
            this.client = new RequestCacheClient();
        }

        protected override ICacheClient Client { get { return client; } }

    }
}
