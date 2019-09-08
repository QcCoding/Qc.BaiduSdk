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
    public class IdcardModel : PageModel
    {
        private readonly BaiduOcrService _baiduOcrService;
        public IdcardModel(BaiduOcrService baiduOcrService)
        {
            _baiduOcrService = baiduOcrService;
        }
        /// <summary>
        /// base64图片
        /// </summary>
        [BindProperty]
        public string Base64Str { get; set; }

        public void OnGet()
        {

        }

        /// <summary>
        /// 识别base64图片
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostIdCardBaseStr()
        {
            var result = _baiduOcrService.Idcard<dynamic>(null, Base64Str, "front", true);
            return new JsonResult(result, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}