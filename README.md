# 百度云 SDK

对百度云平台的接口根据需要进行了 sdk 封装，[百度官方的 sdk](https://github.com/Baidu-AIP/dotnet-sdk)

- Ocr Sdk [![Nuget](https://img.shields.io/nuget/v/Qc.BaiduOcrSdk)](https://www.nuget.org/packages/Qc.BaiduOcrSdk/)

## Qc.BaiduOcrSdk

`Qc.BaiduOcrSdk` 时一个基于 `.NET Standard 2.0` 构建，对百度只能云平台的 Ocr 常用接口进行了封装。

使用时注意：

> sdk 中未验证数据的合法性，请保证传递的数据正确
> 图像数据，base64 编码后进行 urlencode，要求 base64 编码和 urlencode 后大小不超过 4M，最短边至少 15px，最长边最大 4096px,
> 支持 jjpg/jpeg/png/bmp 格式，当 image 字段存在时 url 字段失效

### 使用 Qc.BaiduOcrSdk

目前暴露的方法如下，方法暴露了 AccessToken,若为空则默认依次获取已存在的，若没有则重新调用 GetAccessToken 获取

- 获取 AccessToken (GetAccessToken),通过调用`IBaiduOcrSdkHook`保存 token 信息，默认保存到./AppData/{apikey}.txt 文件
- 通用文字识别(文字/精度/位置/生僻字)
- 银行卡识别(Bankcard)
- 身份证识别(Idcard)
- 火车票识别(TrainTicket)
- 其他接口的快速调用(CommonApi)

#### 一.安装程序包

[![Nuget](https://img.shields.io/nuget/v/Qc.BaiduOcrSdk)](https://www.nuget.org/packages/Qc.BaiduOcrSdk/)

- dotnet cli  
  `dotnet add package Qc.BaiduOcrSdk`
- 包管理器  
  `Install-Package Install-Package Qc.BaiduOcrSdk`

#### 二.添加配置

> 如需实现自定义存储 AccessToken，动态获取应用配置，可自行实现接口 `IBaiduOcrSdkHook`  
> 默认提供 `DefaultBaiduOcrSdkHook`，存储 AccessToken 等信息到指定目录(默认./AppData)

```cs
using Qc.BaiduOcrSdk
public void ConfigureServices(IServiceCollection services)
{
  //...
  services.AddBaiduOcrSdk<BaiduOcrSdk.DefaultBaiduOcrSdkHook>(opt =>
  {
      opt.ApiKey = "Api Key";
      opt.SecretKey = "Secret Key";
  });
  //...
}
```

#### 三.代码中使用

在需要地方注入`BaiduOcrService`后即可使用

```cs
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
```

### BaiduOcrConfig 配置项

| 字段名           |  类型  |                                   描述 |
| ---------------- | :----: | -------------------------------------: |
| AppId            | string |                                应用 ID |
| ApiKey           | string |                               应用密钥 |
| SecretKey        | string |                               应用标识 |
| SaveTokenDirPath | string |          token 保存目录 默认 ./AppData |
| ApiUrl           | string | 接口地址 默认 https://aip.baidubce.com |
| Timeout          |  int   |                       接口超时时间 30s |

具体使用可[参考示例](./Qc.BaiduSdk.Sample/Pages/Ocr/)

Baidu Ocr 文档地址: https://ai.baidu.com/docs#/OCR-API/top

## 示例说明

`Qc.BaiduSdk.Sample` 为示例项目，可进行测试
