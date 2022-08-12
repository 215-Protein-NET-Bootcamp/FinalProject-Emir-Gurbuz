namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }
}
