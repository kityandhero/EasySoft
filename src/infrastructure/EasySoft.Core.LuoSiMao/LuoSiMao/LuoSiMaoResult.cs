namespace EasySoft.Core.LuoSiMao.LuoSiMao;

/// <summary>
/// LuoSiMao短信接口返回结果
/// </summary>
public class LuoSiMaoResult
{
    /// <summary>
    /// 错误代码
    /// </summary>
    public string error { get; set; }

    /// <summary>
    /// 返回信息
    /// </summary>
    public string msg { get; set; }

    public LuoSiMaoResult()
    {
        error = "";
        msg = "";
    }
}