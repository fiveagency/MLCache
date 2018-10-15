using System;

namespace MLCache
{
    /// <summary>
    /// Class represents generic cache entry.
    /// </summary>
    [Serializable]
    public class CacheEntry
    {
        public object Value { get; set; }

        public KeyCacheDependency Dependencies { get; set; }

        public DateTime? AbsoluteExpiration { get; set; }
        
        public CacheEntry(object value, KeyCacheDependency dependencies, DateTime? absoluteExpiration)
        {
            Value = value;
            Dependencies = dependencies;
            AbsoluteExpiration = absoluteExpiration;
        }

        public override string ToString()
        {
            return Value != null ? Value.GetType().FullName : "null";
        }
    }
}
