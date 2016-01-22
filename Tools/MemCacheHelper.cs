using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class MemCacheHelper
    {
        static readonly object padlock = new object();
        private static MemcachedClient MemClient;
        private static MemcachedClient MemClientInit()
	    {
            return new MemcachedClient("enyim.com/memcached");
	    }
        public static MemcachedClient getInstance()
        {
            if (MemClient == null)
            {
                lock (padlock)
                {
                    if (MemClient == null)
                    {
                       MemClient= MemClientInit();
                    }
                }
            }
            return MemClient;
        }
        public static void SetMemCache(string key, object value)
        {
            MemClient = getInstance();
            MemClient.Store(StoreMode.Set, key, value);
        }
        public static object GetMemCache(string key)
        {
            MemClient = getInstance();
            return MemClient.Get(key);
        }
       
    }
}
