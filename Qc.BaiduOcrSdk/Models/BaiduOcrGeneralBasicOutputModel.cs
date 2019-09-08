using System;
using System.Collections.Generic;
using System.Text;

namespace Qc.BaiduOcrSdk.Models
{
    public class BaiduOcrGeneralBasicOutputModel : BaiduApiBaseModel
    {
        /// <summary>
        /// 图像方向，当detect_direction=true时存在。
        /// - -1:未定义，
        /// - 0:正向，
        /// - 1: 逆时针90度，
        /// - 2:逆时针180度，
        /// - 3:逆时针270度
        /// </summary>
        public int Direction { get; set; }
        /// <summary>
        /// 唯一的log id，用于问题定位
        /// </summary>
        public long Log_Id { get; set; }
        /// <summary>
        /// 识别结果数组
        /// </summary>
        public List<BaiduOcrGeneralBasicWordsResultModel> Words_Result { get; set; }
        /// <summary>
        /// 识别结果数，表示words_result的元素个数
        /// </summary>
        public int Words_Result_Num { get; set; }
        /// <summary>
        /// 识别结果中每一行的置信度值，包含average：行置信度平均值，variance：行置信度方差，min：行置信度最小值
        /// </summary>
        public float Probability { get; set; }
        /// <summary>
        /// 当detect_language=true时存在
        /// </summary>
        public int Language { get; set; }
    }
    public class BaiduOcrGeneralBasicWordsResultModel
    {
        /// <summary>
        /// 结果
        /// </summary>
        public string Words { get; set; }
    }
}
