using System;
using System.Collections.Generic;
using System.Text;

namespace Qc.BaiduOcrSdk
{

    public class BaiduOcrConfig : BaiduBaseConfig
    {
        /// <summary>
        /// 接口地址
        /// </summary>
        public string ApiUrl { get; set; } = "https://aip.baidubce.com";
        /// <summary>
        /// 请求超时时间
        /// </summary>
        public int? Timeout { get; set; } = 30;
        /// <summary>
        /// token保存目录 默认 ./AppData
        /// </summary>
        public string SaveTokenDirPath { get; set; } = "./AppData";
    }
}
