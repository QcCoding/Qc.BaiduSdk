using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;

namespace Qc.BaiduOcrSdk
{
    public static class BuilderExtensions
    {
        public static IApplicationBuilder UseBaiduOcrSdk(this IApplicationBuilder app, Func<BaiduOcrConfig> configHandler)
        {
            return app;
        }
    }
}
