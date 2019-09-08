using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qc.BaiduOcrSdk
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加百度Orc SDK，注入自定义实现的IBaiduOcrSdkHook
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddBaiduOcrSdk(this IServiceCollection services, Action<BaiduOcrConfig> optionsAction)
        {
            services.AddBaiduOcrSdk<DefaultBaiduOcrSdkHook>(optionsAction);

            return services;
        }
        /// <summary>
        /// 添加百度Orc SDK
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddBaiduOcrSdk<T>(this IServiceCollection services, Action<BaiduOcrConfig> optionsAction = null) where T : class, IBaiduOcrSdkHook
        {
            if (optionsAction != null)
            {
                services.Configure(optionsAction);
            }
            services.AddScoped<IBaiduOcrSdkHook, T>();
            services.AddScoped<BaiduOcrService, BaiduOcrService>();
            services.AddHttpClient();
            return services;
        }
    }
}
