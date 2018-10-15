namespace MLCache
{
    /// <summary>
    /// Supports interoperability between cache providers.
    /// </summary>
    public interface ICacheInterop
    {
        /// <summary>
        /// Retrieves the specified entry from the cache.
        /// </summary>
        /// <param name="key">The identifier for the cache entry to retrieve.</param>
        /// <returns>The retrieved cache entry, or a null reference if the
        /// key is not found.</returns>
        CacheEntry GetEntry(string key);
    }
}
