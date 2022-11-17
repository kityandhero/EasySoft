namespace EasySoft.UtilityTools.Standard.Securities.Encryption;

/// <summary>
/// Sha1Assist
/// </summary>
public static class HashAssist
{
    /// <summary>
    /// Sha1 加密
    /// </summary>
    /// <param name="value">加密字符串</param>
    /// <returns></returns>
    public static string ToSha1(string value)
    {
        return Encrypt(value, "sha1");
    }

    /// <summary>
    /// Sha256 加密
    /// </summary>
    /// <param name="value">加密字符串</param>
    /// <returns></returns>
    public static string ToSha256(string value)
    {
        return Encrypt(value, "sha256");
    }

    /// <summary>
    /// Sha384 加密
    /// </summary>
    /// <param name="value">加密字符串</param>
    /// <returns></returns>
    public static string ToSha384(string value)
    {
        return Encrypt(value, "sha384");
    }

    /// <summary>
    /// Sha512 加密
    /// </summary>
    /// <param name="value">加密字符串</param>
    /// <returns></returns>
    public static string ToSha512(string value)
    {
        return Encrypt(value, "sha512");
    }

    private static string Encrypt(string value, string encryptType)
    {
        if (string.IsNullOrEmpty(value)) return value;

        using var hashAlgorithm = HashAlgorithm.Create(encryptType);

        if (hashAlgorithm == null) throw new Exception("hashAlgorithm is null");

        var buffer = Encoding.UTF8.GetBytes(value);

        buffer = hashAlgorithm.ComputeHash(buffer);
        hashAlgorithm.Clear();

        //使用hex格式数据输出
        var result = new StringBuilder();

        foreach (var b in buffer) result.AppendFormat("{0:x2}", b);

        return result.ToString();

        //或者使用下面的输出
        //return BitConverter.ToString(buffer).Replace("-", "").ToLower();
    }
}