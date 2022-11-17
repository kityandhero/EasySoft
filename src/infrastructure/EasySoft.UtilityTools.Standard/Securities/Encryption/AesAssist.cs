namespace EasySoft.UtilityTools.Standard.Securities.Encryption;

/// <summary>
/// AesAssist
/// </summary>
public static class AesAssist
{
    #region AES

    /// <summary>
    /// 创建算法操作对象
    /// </summary>
    /// <param name="secretKey"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    private static RijndaelManaged CreateRijndaelManaged(string secretKey, string iv)
    {
        var rijndaelManaged = new RijndaelManaged();

        rijndaelManaged.Padding = PaddingMode.PKCS7;
        rijndaelManaged.KeySize = 128;
        rijndaelManaged.BlockSize = 128;

        rijndaelManaged.Mode = CipherMode.CBC;

        var secretBytes = Encoding.UTF8.GetBytes(secretKey);
        var keyBytes = new byte[16];

        Array.Copy(
            secretBytes,
            keyBytes,
            Math.Min(secretBytes.Length, keyBytes.Length)
        );

        rijndaelManaged.Key = keyBytes;

        if (string.IsNullOrEmpty(iv))
        {
            rijndaelManaged.Mode = CipherMode.ECB;
        }
        else
        {
            rijndaelManaged.Mode = CipherMode.CBC;

            var array = Encoding.UTF8.GetBytes(iv);
            var ivBytes = new byte[keyBytes.Length];

            Array.Copy(
                array,
                ivBytes,
                Math.Min(array.Length, ivBytes.Length)
            );

            rijndaelManaged.IV = ivBytes;
        }

        return rijndaelManaged;
    }

    /// <summary>
    /// Aes加密
    /// </summary>
    /// <param name="value"></param>
    /// <param name="secretKey"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string Encrypt(string value, string secretKey, string iv)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;

        using var rijndaelManaged = CreateRijndaelManaged(secretKey, iv);
        using var iCryptoTransform = rijndaelManaged.CreateEncryptor();

        var buffer = Encoding.UTF8.GetBytes(value);

        buffer = iCryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length);

        //使用hex格式输出数据
        var result = new StringBuilder();

        foreach (var b in buffer) result.AppendFormat("{0:x2}", b);

        return result.ToString();
        //或者使用下面的输出
        //return BitConverter.ToString(buffer).Replace("-", "").ToLower();
    }

    /// <summary>
    /// Aes解密
    /// </summary>
    /// <param name="value"></param>
    /// <param name="secretKey"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string Decrypt(string value, string secretKey, string iv)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;

        using var rijndaelManaged = CreateRijndaelManaged(secretKey, iv);

        using var iCryptoTransform = rijndaelManaged.CreateDecryptor();

        //转换hex格式数据为byte数组
        var buffer = new byte[value.Length / 2];

        for (var i = 0; i < buffer.Length; i++)
            buffer[i] = (byte)Convert.ToInt32(value.Substring(i * 2, 2), 16);

        buffer = iCryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length);

        return Encoding.UTF8.GetString(buffer);
    }

    #endregion
}