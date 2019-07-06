using Microsoft.Extensions.Options;

namespace Aju.Carefree.Cache
{
    public class ServiceStackRedisCacheOptions : IOptions<ServiceStackRedisCacheOptions>
    {
        public string Host { get; set; }

        public int Port { get; set; } = 6379;

        public string Password { get; set; }

        ServiceStackRedisCacheOptions IOptions<ServiceStackRedisCacheOptions>.Value
        {
            get { return this; }
        }
    }
}
