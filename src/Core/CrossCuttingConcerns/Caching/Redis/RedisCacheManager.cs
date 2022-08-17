using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly RedisConfiguration _redisConfiguration;
        public RedisCacheManager(IConfiguration configuration)
        {
            _redisConfiguration = configuration.GetSection("RedisConfiguration").Get<RedisConfiguration>();
        }

        public void Add(string key, object value, int duration)
        {
            if (value is Task)
            {
                value = (value as dynamic).Result;
            }
            redisInvoker((x) => x.Add(key, value, TimeSpan.FromMinutes(duration)));
        }

        public object Get(string key)
        {
            object result = default;
            redisInvoker((x) => result = x.Get(key));
            return result;
        }

        public T Get<T>(string key)
        {
            T result = default;
            redisInvoker((X) => result = X.Get<T>(key));
            return result;
        }

        public bool IsAdd(string key)
        {
            bool isAdd = false;
            redisInvoker((x) => isAdd = x.ContainsKey(key));
            return isAdd;
        }

        public void Remove(string key)
        {
            redisInvoker((x) => x.Remove(key));
        }

        public void RemoveByPattern(string pattern)
        {
            redisInvoker((x) => x.RemoveByPattern(pattern));
        }

        private void redisInvoker(Action<RedisClient> redisAction)
        {
            using (var client = new RedisClient(_redisConfiguration.Host, _redisConfiguration.Port, _redisConfiguration.Password))
            {
                redisAction.Invoke(client);
            }
        }
    }
}
