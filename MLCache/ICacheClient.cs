using System;

namespace MLCache
{
    /// <summary>
    /// Cache client interface.
    /// </summary>
    public interface ICacheClient
    {
        /// <summary>
        /// Retrieve object from client.
        /// </summary>
        /// <param name="key">Identifier to use for object lookup.</param>
        /// <returns>Object if client contains key or null.</returns>
        object Get(string key);

        /// <summary>
        /// Inserts object into client. 
        /// </summary>
        /// <param name="key">Identifier under which object will be stored.</param>
        /// <param name="value">The object to insert.</param>
        /// <param name="absoluteExpiration">The time at which object should be removed from cache.</param>
        /// <returns>True if object is inserted, otherwise false.</returns>
        bool Insert(string key, object value, DateTime? absoluteExpiration);

        /// <summary>
        /// Remove object from client.
        /// </summary>
        /// <param name="key">Identifier to use for object removal.</param>
        void Remove(string key);

        /// <summary>
        /// Clear client.
        /// </summary>
        void Clear();

    }
}
