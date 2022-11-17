namespace EasySoft.UtilityTools.Standard.Securities.Encryption;

/// <summary>
/// Md5Assist
/// </summary>
public static class Md5Assist
{
    /// <summary>
    /// ToMd5
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToMd5(string source)
    {
        using var md5 = MD5.Create();

        var result = md5.ComputeHash(Encoding.ASCII.GetBytes(source));
        var strResult = BitConverter.ToString(result);

        return strResult.Replace("-", "").ToLower();
    }

    /// <summary>
    /// Verify
    /// </summary>
    /// <param name="data"></param>
    /// <param name="sign"></param>
    /// <returns></returns>
    public static bool Verify(string data, string sign)
    {
        return ToMd5(data) == sign;
    }
}