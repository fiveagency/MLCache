namespace MLCache
{
    /// <summary>
    /// Artificial interface under which chainable cache provider 
    /// can be registered. Usually there
    /// should be only cache provider which registers ICacheProvider
    /// interface, but some providers like MultiLevelCacheProvider
    /// requires cache providers for each level. These levels can 
    /// be then registered using IChainableCacheProvider interface. 
    /// </summary>
    public interface IChainableCacheProvider : ICacheProvider, ICacheInterop
    {
    }
}
