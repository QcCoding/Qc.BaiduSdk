using Microsoft.Extensions.Options;
using Qc.BaiduOcrSdk.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Qc.BaiduOcrSdk
{
    public class BaiduOcrService
    {
        private readonly HttpClient _httpClient;
        private readonly BaiduOcrConfig _baiduApiConfig;
        private readonly IBaiduOcrSdkHook _baiduOcrSdkHook;
        public BaiduOcrService(IHttpClientFactory _httpClientFactory
            , IBaiduOcrSdkHook baiduOcrSdkHook
            )
        {
            _baiduOcrSdkHook = baiduOcrSdkHook;
            _baiduApiConfig = baiduOcrSdkHook.GetConfig();
            if (_baiduApiConfig == null)
                throw new Exception("BaiduOcr未配置");
            _httpClient = _httpClientFactory.CreateClient("baiduocr");
            _httpClient.BaseAddress = new Uri(_baiduApiConfig.ApiUrl);
            if (_baiduApiConfig.Timeout.HasValue)
                _httpClient.Timeout = TimeSpan.FromSeconds(_baiduApiConfig.Timeout.Value);
        }
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private string GetOrUpdateAccessToken(string token)
        {
            var access_token = token ?? _baiduOcrSdkHook.GetTokenInfo(_baiduApiConfig.ApiKey)?.Access_Token ?? GetAccessToken().Access_Token;
            if (string.IsNullOrEmpty(access_token))
                throw new Exception("获取授权失败");
            return HttpUtility.UrlEncode(access_token);
        }
        /// <summary>
        /// 替换base64图片
        /// </summary>
        /// <param name="base64str"></param>
        /// <returns></returns>
        private string ReplaceImageBase64(string base64str)
        {
            if (string.IsNullOrEmpty(base64str))
                return base64str;
            return System.Web.HttpUtility.UrlEncode(Regex.Replace(base64str, "data:image/.*;base64,", ""));
        }
        /// <summary>
        /// 获取 AccessToken
        /// </summary>
        /// <param name="skipSave">跳过保存</param>
        /// <returns></returns>
        public BaiduAccessTokenModel GetAccessToken(bool skipSave = false)
        {
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", _baiduApiConfig.ApiKey));
            paraList.Add(new KeyValuePair<string, string>("client_secret", _baiduApiConfig.SecretKey));
            var responseResult = _httpClient.HttpPostParams<BaiduAccessTokenModel>("/oauth/2.0/token", paraList);
            if (responseResult.IsError())
                return null;
            if (responseResult.Expires_In.HasValue)
                responseResult.ExpiresEndTime = DateTime.Now.AddSeconds(responseResult.Expires_In.Value);
            responseResult.ApiKey = _baiduApiConfig.ApiKey;
            if (skipSave)
            {
                return responseResult;
            }
            return _baiduOcrSdkHook.SaveTokenInfo(responseResult);
        }
        /// <summary>
        /// 通用文字识别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public T GeneralBasic<T>(BaiduOcrGeneralBasicInputModel model)
        {
            var access_token = GetOrUpdateAccessToken(model.AccessToken);
            var dicData = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(model.Image))
                dicData.Add("image", ReplaceImageBase64(model.Image));
            if (!string.IsNullOrEmpty(model.Url))
                dicData.Add("url", model.Url);
            if (!string.IsNullOrEmpty(model.Language_Type))
                dicData.Add("language_type", model.Language_Type);
            if (!model.Detect_Direction)
                dicData.Add("detect_direction", model.Detect_Direction.ToString().ToLower());
            if (!model.Detect_Language)
                dicData.Add("detect_language", model.Detect_Language.ToString().ToLower());
            if (!model.Probability)
                dicData.Add("probability", model.Probability.ToString().ToLower());
            var responseResult = _httpClient.HttpPost<T>("/rest/2.0/ocr/v1/general_basic?access_token=" + access_token, dicData);
            return responseResult;
        }
        /// <summary>
        /// 通用文字识别（高精度含位置版）
        /// </summary>
        /// <returns></returns>
        public T Accurate<T>(BaiduOcrAccurateInputModel model)
        {
            var access_token = GetOrUpdateAccessToken(model.AccessToken);
            var dicData = new Dictionary<string, object>();
            dicData.Add("image", ReplaceImageBase64(model.Image));
            if (!string.IsNullOrEmpty(model.Recognize_Granularity))
                dicData.Add("recognize_granularity", model.Recognize_Granularity);
            if (!model.Detect_Direction)
                dicData.Add("detect_direction", model.Detect_Direction.ToString().ToLower());
            if (!model.Vertexes_Location)
                dicData.Add("vertexes_location", model.Vertexes_Location.ToString().ToLower());
            if (!model.Probability)
                dicData.Add("probability", model.Probability.ToString().ToLower());
            var responseResult = _httpClient.HttpPost<T>("/rest/2.0/ocr/v1/accurate?access_token=" + access_token, dicData);
            return responseResult;
        }
        /// <summary>
        /// 通用文字识别（含位置信息版）
        /// </summary>
        /// <returns></returns>
        public T General<T>(BaiduOcrGeneralBasicInputModel model)
        {
            var access_token = GetOrUpdateAccessToken(model.AccessToken);
            var dicData = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(model.Image))
                dicData.Add("image", ReplaceImageBase64(model.Image));
            if (!string.IsNullOrEmpty(model.Url))
                dicData.Add("url", model.Url);
            if (!string.IsNullOrEmpty(model.Recognize_Granularity))
                dicData.Add("recognize_granularity", model.Recognize_Granularity);
            if (!string.IsNullOrEmpty(model.Language_Type))
                dicData.Add("language_type", model.Language_Type);
            if (!model.Detect_Direction)
                dicData.Add("detect_direction", model.Detect_Direction.ToString().ToLower());
            if (!model.Detect_Language)
                dicData.Add("detect_language", model.Detect_Language.ToString().ToLower());
            if (!model.Probability)
                dicData.Add("probability", model.Probability.ToString().ToLower());
            var responseResult = _httpClient.HttpPost<T>("/rest/2.0/ocr/v1/general?access_token=" + access_token, dicData);
            return responseResult;
        }
        /// <summary>
        /// 通用文字识别（高精度版）
        /// </summary>
        /// <returns></returns>
        public T AccurateBasic<T>(BaiduOcrAccurateInputModel model)
        {
            var access_token = GetOrUpdateAccessToken(model.AccessToken);
            var dicData = new Dictionary<string, object>();
            dicData.Add("image", ReplaceImageBase64(model.Image));
            if (!model.Detect_Direction)
                dicData.Add("detect_direction", model.Detect_Direction.ToString().ToLower());
            if (!model.Probability)
                dicData.Add("probability", model.Probability.ToString().ToLower());
            var responseResult = _httpClient.HttpPost<T>("/rest/2.0/ocr/v1/accurate_basic?access_token=" + access_token, dicData);
            return responseResult;
        }
        /// <summary>
        /// 通用文字识别（含生僻字版）
        /// </summary>
        /// <returns></returns>
        public T GeneralEnhanced<T>(BaiduOcrAccurateInputModel model)
        {
            var access_token = GetOrUpdateAccessToken(model.AccessToken);
            var dicData = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(model.Image))
                dicData.Add("image", ReplaceImageBase64(model.Image));
            if (!string.IsNullOrEmpty(model.Url))
                dicData.Add("url", model.Url);
            if (!model.Detect_Direction)
                dicData.Add("detect_direction", model.Detect_Direction.ToString().ToLower());
            if (!model.Probability)
                dicData.Add("probability", model.Probability.ToString().ToLower());
            var responseResult = _httpClient.HttpPost<T>("/rest/2.0/ocr/v1/general_enhanced?access_token=" + access_token, dicData);
            return responseResult;
        }
        /// <summary>
        /// 银行卡识别
        /// </summary>
        /// <param name="accessToken">通过API Key和Secret Key获取的access_token,</param>
        /// <param name="image">图像数据，将自动对iamge进行编码，编码后的大小不能超过4M，最短边至少15px，最长边最大4096px,支持jpg/jpeg/png/bmp格式</param>
        /// <remarks>
        /// 识别银行卡并返回卡号、有效期、发卡行和卡片类型。
        /// </remarks>
        /// <returns></returns>
        public T Bankcard<T>(string accessToken, string image)
        {
            var access_token = GetOrUpdateAccessToken(accessToken);
            var dicData = new Dictionary<string, object>();
            dicData.Add("image", ReplaceImageBase64(image));
            var responseResult = _httpClient.HttpPost<T>("/rest/2.0/ocr/v1/bankcard?access_token=" + access_token, dicData);
            return responseResult;
        }
        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="accessToken">通过API Key和Secret Key获取的access_token,</param>
        /// <param name="image">图像数据，将自动对iamge进行编码，编码后的大小不能超过4M，最短边至少15px，最长边最大4096px,支持jpg/jpeg/png/bmp格式</param>
        /// <param name="id_card_side">front：身份证含照片的一面；back：身份证带国徽的一面</param>
        /// <param name="detect_direction">是否检测图像旋转角度，默认检测，即：true。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:- true：检测旋转角度；- false：不检测旋转角度。</param>
        /// <param name="detect_risk">是否开启身份证风险类型(身份证复印件、临时身份证、身份证翻拍、修改过的身份证)功能，默认不开启，即：false。可选值:true-开启；false-不开启</param>
        /// <returns></returns>
        public T Idcard<T>(string accessToken, string image, string id_card_side, bool detect_direction = false, bool detect_risk = false)
        {
            var access_token = GetOrUpdateAccessToken(accessToken);
            var dicData = new Dictionary<string, object>();
            dicData.Add("image", ReplaceImageBase64(image));
            dicData.Add("id_card_side", id_card_side);
            if (!detect_direction)
                dicData.Add("detect_direction", detect_direction.ToString().ToLower());
            if (!detect_risk)
                dicData.Add("detect_risk", detect_risk.ToString().ToLower());
            var responseResult = _httpClient.HttpPost<T>("/rest/2.0/ocr/v1/idcard?access_token=" + access_token, dicData);
            return responseResult;
        }

        /// <summary>
        /// 火车票识别
        /// </summary>
        /// <param name="accessToken">通过API Key和Secret Key获取的access_token,</param>
        /// <param name="image">图像数据，将自动对iamge进行编码，编码后的大小不能超过4M，最短边至少15px，最长边最大4096px,支持jpg/jpeg/png/bmp格式</param>
        /// <remarks>
        /// 支持对大陆火车票的车票号、始发站、目的站、车次、日期、票价、席别、姓名进行结构化识别
        /// </remarks>
        /// <returns></returns>
        public T TrainTicket<T>(string accessToken, string image)
        {
            var access_token = GetOrUpdateAccessToken(accessToken);
            var dicData = new Dictionary<string, object>();
            dicData.Add("image", ReplaceImageBase64(image));
            var responseResult = _httpClient.HttpPost<T>("/rest/2.0/ocr/v1/train_ticket?access_token=" + access_token, dicData);
            return responseResult;
        }


        /// <summary>
        /// 通用封装
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <param name="accessToken"></param>
        /// <param name="dicData"></param>
        /// <returns></returns>
        public T CommonApi<T>(string apiUrl, string accessToken, Dictionary<string, object> dicData)
        {
            var access_token = GetOrUpdateAccessToken(accessToken);
            var responseResult = _httpClient.HttpPost<T>($"{apiUrl}?access_token={access_token}", dicData);
            return responseResult;
        }
    }
}
