using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Qc.BaiduOcrSdk;
using Qc.BaiduOcrSdk.Models;

namespace Qc.BaiduSdk.Sample.Pages.Ocr
{
    public class GeneralBasicModel : PageModel
    {
        private readonly BaiduOcrService _baiduOcrService;
        public GeneralBasicModel(BaiduOcrService baiduOcrService)
        {
            _baiduOcrService = baiduOcrService;
        }
        /// <summary>
        /// base64图片
        /// </summary>
        [BindProperty]
        public string Base64Str { get; set; }
        /// <summary>
        /// 远程图片
        /// </summary>
        [BindProperty]
        public string RemoteUrl { get; set; }


        /// <summary>
        /// 识别base64图片
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostGeneralBasicBaseStr()
        {
            var result = _baiduOcrService.GeneralBasic<dynamic>(new BaiduOcrGeneralBasicInputModel()
            {
                Image = Base64Str
            });
            return new JsonResult(result, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
        /// <summary>
        /// 识别远程图片
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostGeneralBasicRemoteUrl()
        {
            var result = _baiduOcrService.GeneralBasic<dynamic>(new BaiduOcrGeneralBasicInputModel()
            {
                Url = RemoteUrl
            });
            return new JsonResult(result, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}