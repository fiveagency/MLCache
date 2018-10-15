namespace MLCache.Memory
{
    public class MemoryCacheProvider : AbstractDependencyCacheProvider
    {
        private readonly MemoryCacheClient client;

        public MemoryCacheProvider(int? maxDurationSeconds = null)
        {
            this.client = new MemoryCacheClient(maxDurationSeconds);
        }

        protected override ICacheClient Client { get { return client; } }
    }
}
