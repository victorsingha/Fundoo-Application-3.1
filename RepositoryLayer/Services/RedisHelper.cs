using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class RedisHelper
    {
        public static void SaveData(string host,string key,string value)
        {
            using (RedisClient client = new RedisClient(host))
            {
                if(client.Get<string>(key) == null)
                {
                    client.Set(key, value);
                }
            }
        }

        public static string ReadData(string host,string key)
        {
            using (RedisClient client = new RedisClient(host))
            {
                return client.Get<string>(key);
            }
        }
    }
}
