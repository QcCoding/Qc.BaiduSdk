using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Qc.BaiduOcrSdk;

namespace Qc.BaiduSdk.Sample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BaiduOcrService _baiduOcrService;
        public IndexModel(BaiduOcrService baiduOcrService)
        {
            _baiduOcrService = baiduOcrService;
        }
        public IActionResult OnPostGetAccessToken()
        {
            var result = _baiduOcrService.GetAccessToken();
            return new JsonResult(result, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}
