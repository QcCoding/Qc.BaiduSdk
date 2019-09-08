using System;
using System.Collections.Generic;
using System.Text;

namespace Qc.BaiduOcrSdk.Models
{
    public class BaiduApiBaseModel
    {
        /// <summary>
        /// 错误码 失败返回 invalid_client
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error_Description { get; set; }
    }
}
