using Qc.BaiduOcrSdk.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Qc.BaiduOcrSdk
{
    public static class BaiduApiExtension
    {
        public static bool IsError(this BaiduApiBaseModel input)
        {
            return !string.IsNullOrEmpty(input.Error);
        }
        public static bool IsSuccess(this BaiduApiBaseModel input)
        {
            return string.IsNullOrEmpty(input.Error);
        }
    }
}
