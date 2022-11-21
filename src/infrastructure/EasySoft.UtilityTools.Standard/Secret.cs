using EasySoft.UtilityTools.Standard.Media.Image;
using EasySoft.UtilityTools.Standard.Securities.Encryption;

namespace EasySoft.UtilityTools.Standard;

/// <summary>
/// Secret
/// </summary>
public class Secret
{
    private string Key { get; }

    /// <summary>
    /// Secret
    /// </summary>
    /// <param name="key"></param>
    public Secret(string key)
    {
        Key = key;
    }

    /// <summary>
    /// Encrypt
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string Encrypt(string source)
    {
        if (string.IsNullOrWhiteSpace(source)) throw new Exception("空字符串不允许加密");

        var c = new Captcha
        {
            LetterCount = 4
        };
        source = c.GetRandomString() + source + RandomEx.CreateRandomCode(4);

        var result = DesAssist.Encrypt(source, Key);

        result = result.Replace("=", "")
            .Replace("+", "-")
            .Replace("/", "@");

        return result;
    }

    /// <summary>
    /// EncryptWithExpirationTime
    /// </summary>
    /// <param name="source"></param>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public string EncryptWithExpirationTime(string source, TimeSpan timeSpan)
    {
        var time = DateTime.Now.Add(timeSpan);
        return EncryptWithExpirationTime(source, time);
    }

    /// <summary>
    /// EncryptWithExpirationTime
    /// </summary>
    /// <param name="source"></param>
    /// <param name="expirationTime"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string EncryptWithExpirationTime(string source, DateTime expirationTime)
    {
        if (string.IsNullOrWhiteSpace(source)) throw new Exception("空字符串不允许加密");

        var c = new Captcha
        {
            LetterCount = 4
        };

        source = c.GetRandomString() + expirationTime.ToString(
            "yyyy-MM-dd HH:mm:ss"
        ) + source + RandomEx.CreateRandomCode(4);

        var result = DesAssist.Encrypt(source, Key);

        result = result.Replace("=", "")
            .Replace("+", "-")
            .Replace("/", "@");

        return result;
    }

    /// <summary>
    /// Decrypt
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public string Decrypt(string target)
    {
        var result = "";

        if (string.IsNullOrWhiteSpace(target)) return result;

        target = target.Replace("%25", "%").Replace("%40", "@");
        target = target.Replace("-", "+").Replace("@", "/");

        var residue = target.Length % 4;

        if (residue > 0)
        {
            var complement = 4 - residue;

            for (var i = 0; i < complement; i++) target += "=";
        }

        result = DesAssist.Decrypt(target, Key);
        result = result.Substring(0, result.Length - 4);
        result = result.Substring(4);

        return result;
    }

    /// <summary>
    /// DecryptWithExpirationTime
    /// </summary>
    /// <param name="target"></param>
    /// <param name="expired"></param>
    /// <returns></returns>
    public string DecryptWithExpirationTime(string target, out bool expired)
    {
        return DecryptWithExpirationTime(target, out expired, out var _);
    }

    /// <summary>
    /// DecryptWithExpirationTime
    /// </summary>
    /// <param name="target"></param>
    /// <param name="expired"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public string DecryptWithExpirationTime(string target, out bool expired, out DateTime time)
    {
        var result = "";
        time = DateTime.Now.AddDays(-1);

        expired = false;

        if (string.IsNullOrWhiteSpace(target)) return result;

        target = target.Replace("%25", "%").Replace("%40", "@");
        target = target.Replace("-", "+").Replace("@", "/");

        var residue = target.Length % 4;

        if (residue > 0)
        {
            var complement = 4 - residue;

            for (var i = 0; i < complement; i++) target += "=";
        }

        result = DesAssist.Decrypt(target, Key);
        result = result.Substring(0, result.Length - 4);
        result = result.Substring(4);

        var timeString = result.Substring(0, 19);

        if (DateTime.TryParse(timeString, out time))
        {
            if (time.Subtract(DateTime.Now).Milliseconds < 0) expired = true;
        }
        else
        {
            expired = true;
        }

        result = result.Substring(19);

        return result;
    }
}