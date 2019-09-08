using System;
using System.Collections.Generic;
using System.Text;

namespace Qc.BaiduOcrSdk.Models
{
    /// <summary>
    /// AccessToken返回模型
    /// </summary>
    public class BaiduAccessTokenModel : BaiduApiBaseModel
    {
        /// <summary>
        /// 获取的Access Token
        /// </summary>
        public string Refresh_Token { get; set; }
        /// <summary>
        /// 过期时间(秒) 一般为1个月)
        /// </summary>
        public int? Expires_In { get; set; }
        public string Scope { get; set; }
        public string Session_Key { get; set; }
        public string Access_Token { get; set; }
        public string Session_Secret { get; set; }
        /// <summary>
        /// 自定义字段- 应用的key
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// 自定义- 手动计算 access_token 结束时间
        /// </summary>
        public DateTime? ExpiresEndTime { get; set; }
    }
}
