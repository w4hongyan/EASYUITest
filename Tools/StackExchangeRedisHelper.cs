using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Tools
{
    public static class StackExchangeRedisHelper
    {
       static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
       static IDatabase db;
        static StackExchangeRedisHelper()
        {
            db = redis.GetDatabase();
        }
        public static string StringGet(string key)
        {
            return db.StringGet(key);
        }
        public static bool StringSet(string key, string value)
        {
            return db.StringSet(key, value);
        }
    }
}
