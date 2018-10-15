using System;

namespace MLCache
{
    public interface ICacheProvider
    {
        /// <summary>
        /// Inserts an object into the cache that has file or key dependencies.
        /// </summary>
        /// <param name="key">The cache key used to identify the item.</param>
        /// <param name="value">The object to be inserted in the cache.</param>
        /// <param name="dependencies">The file or cache key dependencies
        /// for the inserted object. When any dependency changes, the object
        /// becomes invalid and is removed from the cache. If there are no
        /// dependencies, this parameter contains a null reference.</param>
        /// <exception cref="System.ArgumentNullException">The key or value
        /// parameter is a null reference.</exception>
        /// <remarks>This method will overwrite an existing Cache item with
        /// the same key parameter.</remarks>
        void Insert(string key, object value, KeyCacheDependency dependencies);

        /// <summary>
        /// Inserts an object into the cache with dependencies and expiration policies.
        /// </summary>
        /// <param name="key">The cache key used to reference the object.</param>
        /// <param name="value">The object to be inserted in the cache.</param>
        /// <param name="dependencies">The file or cache key dependencies for
        /// the inserted object. When any dependency changes, the object
        /// becomes invalid and is removed from the cache. If there are no
        /// dependencies, this parameter contains a null reference.</param>
        /// <param name="absoluteExpiration">The time at which the inserted
        /// object expires and is removed from the cache.</param>
        /// <exception cref="System.ArgumentNullException">The key or value
        /// parameter is a null reference.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">You set
        /// the slidingExpiration parameter to less than TimeSpan.Zero or
        /// the equivalent of more than one year.</exception>
        /// <exception cref="System.ArgumentException">The 
        /// absoluteExpiration and slidingExpiration parameters are both 
        /// set for the item you are trying to add to the Cache.</exception>
        /// <remarks>This method will overwrite an existing Cache item with
        /// the same key parameter.</remarks>
        void Insert(string key, object value, KeyCacheDependency dependencies,
            DateTime absoluteExpiration);

        /// <summary>
        /// Removes the specified item from the cache.
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve.</param>
        void Remove(string key);

        /// <summary>
        /// Retrieves the specified value from the cache.
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve.</param>
        /// <returns>The retrieved cache value, or a null reference if the
        /// key is not found.</returns>
        object GetValue(string key);

        /// <summary>
        /// Removes all cache entries from the cache.
        /// </summary>
        void Clear();
    }
}
