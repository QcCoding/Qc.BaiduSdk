using Qc.BaiduOcrSdk.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qc.BaiduOcrSdk
{
    public interface IBaiduOcrSdkHook
    {
        /// <summary>
        /// 获取OCR配置
        /// </summary>
        /// <returns></returns>
        BaiduOcrConfig GetConfig();
        /// <summary>
        /// 从缓存中获取AccessToken
        /// </summary>
        /// <param name="apiKey">应用接口Key</param>
        /// <returns></returns>
        BaiduAccessTokenModel GetTokenInfo(string apiKey);
        /// <summary>
        /// 保存Token信息
        /// </summary>
        /// <returns></returns>
        BaiduAccessTokenModel SaveTokenInfo(BaiduAccessTokenModel input);
    }
}
