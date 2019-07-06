using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Injector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aju.Carefree.NetCore.IOC
{
    public class AspectCoreContainer
    {
        private static IServiceResolver resolver { get; set; }

        public static IServiceProvider BuildServiceProvider(IServiceCollection services,
            Action<IAspectConfiguration> configure = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.ConfigureDynamicProxy(configure);
            services.AddAspectCoreContainer();
            return resolver = services.ToServiceContainer().Build();
        }

        public static T Resolve<T>()
        {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver), "调用此方法时必须先调用BuildServiceProvider！");
            return resolver.Resolve<T>();
        }
        public static object Resolve(Type type)
        {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver), "调用此方法时必须先调用BuildServiceProvider！");
            return resolver.Resolve(type);
        }
        public static object Resolve(string typeName)
        {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver), "调用此方法时必须先调用BuildServiceProvider！");
            return resolver.Resolve(Type.GetType(typeName));
        }
        public static List<T> ResolveServices<T>()
        {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver), "调用此方法时必须先调用BuildServiceProvider！");
            return resolver.GetServices<T>().ToList();
        }
        public static List<T> ResolveServices<T>(Func<T, bool> filter)
        {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver), "调用此方法时必须先调用BuildServiceProvider！");
            if (filter == null) filter = m => true;
            return ResolveServices<T>().Where(filter).ToList();
        }
    }
}
