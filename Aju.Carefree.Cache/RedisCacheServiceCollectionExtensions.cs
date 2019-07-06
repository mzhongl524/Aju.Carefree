using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aju.Carefree.Cache
{
    public static class RedisCacheServiceCollectionExtensions
    {
        public static IServiceCollection AddDistributedServiceStackRedisCache(this IServiceCollection services,
            Action<ServiceStackRedisCacheOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();
            services.Configure(setupAction);
            services.Add(ServiceDescriptor.Singleton<ICacheService, RedisCacheService>());

            return services;
        }

        public static IServiceCollection AddDistributedServiceStackRedisCache(this IServiceCollection services,
            IConfigurationSection section)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            services.Configure<ServiceStackRedisCacheOptions>(option =>
            {
                //  option.Host = section.GetSection("")[""];
            });

            services.Add(ServiceDescriptor.Transient<ICacheService, RedisCacheService>());

            return services;
        }
    }
}
