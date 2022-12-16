using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.LuoSiMao.LuoSiMao;

/// <summary>
/// 短信服务
/// </summary>
public class SmsService : ISmsService
{
    /// <summary>
    /// 短信配置提供器
    /// </summary>
    private readonly SmsConfig _smsConfig;

    /// <summary>
    /// HttpClientFactory
    /// </summary>
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// 初始化短信服务
    /// </summary>
    /// <param name="smsConfig">短信配置提供器</param>
    /// <param name="httpClientFactory">HttpClientFactory 来自依赖注入</param>
    public SmsService(SmsConfig smsConfig, IHttpClientFactory httpClientFactory)
    {
        _smsConfig = smsConfig;
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// 发送短信
    /// </summary>
    /// <param name="mobile">手机号</param>
    /// <param name="content">内容</param>
    public async Task<SmsResult> SendAsync(string mobile, string content)
    {
        var client = _httpClientFactory.CreateClient();

        var data = new Dictionary<string, object>
        {
            {
                "mobile", mobile
            },
            {
                "message", content
            }
        };

        client.DefaultRequestHeaders.Add("Authorization", await GetAuthorization());

        var mes = await client.PostAsync("https://sms-api.luosimao.com/v1/send.json", data);

        var response = await mes.Content.ReadAsStringAsync();

        return CreateResult(response);
    }

    /// <summary>
    /// 获取授权头信息
    /// </summary>
    private Task<string> GetAuthorization()
    {
        return Task.FromResult(
            $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"api:{_smsConfig.Key}"))}"
        );
    }

    /// <summary>
    /// 创建结果
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private SmsResult CreateResult(string message)
    {
        var result = JsonConvertAssist.DeserializeObject<LuoSiMaoResult>(message);

        if (result == null) throw new Exception("response can not convert to LuoSiMaoResult");

        if (result.error == "0") return SmsResult.Ok;

        if (result.msg == "WRONG_MOBILE") return new SmsResult(false, message, SmsErrorCode.MobileError);

        return new SmsResult(false, message);
    }
}