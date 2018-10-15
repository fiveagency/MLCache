using System;
using System.Net;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace MLCache.Memcached
{
    internal class MemcachedClient : ICacheClient
    {
        private readonly IPAddress serverAddress;
        private readonly int serverPort;
        
        private Enyim.Caching.MemcachedClient client;
        
        public MemcachedClient(IPAddress serverAddress, int serverPort)
        {
            this.serverAddress = serverAddress;
            this.serverPort = serverPort;
        }

        private Enyim.Caching.MemcachedClient Client
        {
            get
            {
                if (client == null)
                {
                    var config = new MemcachedClientConfiguration();
                    var endpoint = new IPEndPoint(serverAddress, serverPort);
                    config.Servers.Add(endpoint);
                    config.Protocol = MemcachedProtocol.Binary;
                    config.KeyTransformer = new TigerHashKeyTransformer();
                    client = new Enyim.Caching.MemcachedClient(config);
                }
                return client;
            }
        }

        public object Get(string key)
        {
            return Client.Get(key);
        }

        public bool Insert(string key, object value, DateTime? absoluteExpiration)
        {
            var success = absoluteExpiration.HasValue ?
                Client.Store(StoreMode.Set, key, value, absoluteExpiration.Value) :
                Client.Store(StoreMode.Set, key, value);

            return success;
        }

        public void Remove(string key)
        {
            Client.Remove(key);
        }

        public void Clear()
        {
            Client.FlushAll();
        }
    }
}
