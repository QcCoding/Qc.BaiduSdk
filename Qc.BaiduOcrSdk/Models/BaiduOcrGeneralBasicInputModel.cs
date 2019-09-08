using System;
using System.Collections.Generic;
using System.Text;

namespace Qc.BaiduOcrSdk.Models
{
    public class BaiduOcrGeneralBasicInputModel
    {
        public string AccessToken { get; set; }

        /// <summary>
        /// 图像数据，将自动对iamge进行编码，编码后的大小不能超过4M，最短边至少15px，最长边最大4096px,支持jjpg/jpeg/png/bmp格式，当image字段存在时url字段失效
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 图片完整URL，URL长度不超过1024字节，URL对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/jpeg/png/bmp格式，当image字段存在时url字段失效，不支持https的图片链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 识别语言类型，默认为CHN_ENG。可选值包括：
        /// - CHN_ENG：中英文混合；
        /// - ENG：英文；
        /// - POR：葡萄牙语；
        /// - FRE：法语；
        /// - GER：德语；
        /// - ITA：意大利语；
        /// - SPA：西班牙语；
        /// - RUS：俄语；
        /// - JAP：日语；
        /// - KOR：韩语
        /// </summary>
        public string Language_Type { get; set; }

        /// <summary>
        /// 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:
        /// - true：检测朝向；
        /// - false：不检测朝向。
        /// </summary>
        public bool Detect_Direction { get; set; }

        /// <summary>
        /// 是否检测语言，默认不检测。当前支持（中文、英语、日语、韩语）
        /// </summary>
        public bool Detect_Language { get; set; }

        /// <summary>
        /// 是否返回识别结果中每一行的置信度
        /// </summary>
        public bool Probability { get; set; }
        /// <summary>
        /// 是否定位单字符位置，big：不定位单字符位置，默认值；small：定位单字符位置
        /// 含位置信息版需要传递
        /// </summary>

        public string Recognize_Granularity { get; set; }
    }
}
