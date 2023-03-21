using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace PersonalFinancialManagement.Common.Extensions
{
    public static class ProxyExtension
    {
        public static void AddProxiedScoped<TInterface, TImplementation>(this IServiceCollection services)
           where TInterface : class
           where TImplementation : class, TInterface
        {
            services.AddScoped<TImplementation>();
            services.AddScoped(typeof(TInterface), serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
                var actual = serviceProvider.GetRequiredService<TImplementation>();
                var interceptors = serviceProvider.GetServices<IAsyncInterceptor>().ToArray();
                return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TInterface), actual, interceptors);
            });
        }

        public static void AddProxiedScoped(this IServiceCollection services, Type TInterface, Type TImplementation)
        {
            services.AddScoped(TImplementation);
            services.AddScoped(TInterface, serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
                var actual = serviceProvider.GetRequiredService(TImplementation);
                var interceptors = serviceProvider.GetServices<IAsyncInterceptor>().ToArray();
                return proxyGenerator.CreateInterfaceProxyWithTarget(TInterface, actual, interceptors);
            });
        }

        public static void AddProxiedTransient<TInterface, TImplementation>(this IServiceCollection services)
           where TInterface : class
           where TImplementation : class, TInterface
        {
            services.AddTransient<TImplementation>();
            services.AddTransient(typeof(TInterface), serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
                var actual = serviceProvider.GetRequiredService<TImplementation>();
                var interceptors = serviceProvider.GetServices<IAsyncInterceptor>().ToArray();
                return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TInterface), actual, interceptors);
            });
        }

        public static void AddProxiedTransient(this IServiceCollection services, Type TInterface, Type TImplementation)
        {
            services.AddTransient(TImplementation);
            services.AddTransient(TInterface, serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
                var actual = serviceProvider.GetRequiredService(TImplementation);
                var interceptors = serviceProvider.GetServices<IAsyncInterceptor>().ToArray();
                return proxyGenerator.CreateInterfaceProxyWithTarget(TInterface, actual, interceptors);
            });
        }

        public static void AddProxiedSingleton<TInterface, TImplementation>(this IServiceCollection services)
           where TInterface : class
           where TImplementation : class, TInterface
        {
            services.AddSingleton<TImplementation>();
            services.AddSingleton(typeof(TInterface), serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
                var actual = serviceProvider.GetRequiredService<TImplementation>();
                var interceptors = serviceProvider.GetServices<IAsyncInterceptor>().ToArray();
                return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TInterface), actual, interceptors);
            });
        }

        public static void AddProxiedSingleton(this IServiceCollection services, Type TInterface, Type TImplementation)
        {
            services.AddSingleton(TImplementation);
            services.AddSingleton(TInterface, serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
                var actual = serviceProvider.GetRequiredService(TImplementation);
                var interceptors = serviceProvider.GetServices<IAsyncInterceptor>().ToArray();
                return proxyGenerator.CreateInterfaceProxyWithTarget(TInterface, actual, interceptors);
            });
        }
    }
}
