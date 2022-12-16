using EasySoft.UtilityTools.Core.Enums;
using EasySoft.UtilityTools.Core.Securities.Interfaces;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// HashHelper
/// </summary>
public static class HashGeneratorExtensions
{
    /// <summary>
    /// 获取哈希之后的字符串
    /// </summary>
    /// <param name="_"></param>
    /// <param name="type">哈希类型</param>
    /// <param name="plaintext">源字符串</param>
    /// <param name="isLower">是否是小写</param>
    /// <returns>哈希算法处理之后的字符串</returns>
    public static string GetHashedString(this IHashGenerator _, HashType type, string plaintext, bool isLower)
    {
        return GetHashedString(_, type, plaintext, Encoding.UTF8, isLower);
    }

    /// <summary>
    /// 获取哈希之后的字符串
    /// </summary>
    /// <param name="_"></param>
    /// <param name="type">哈希类型</param>
    /// <param name="plaintext">源字符串</param>
    /// <param name="encoding">编码类型</param>
    /// <param name="isLower">是否是小写</param>
    /// <returns>哈希算法处理之后的字符串</returns>
    public static string GetHashedString(
        this IHashGenerator _,
        HashType type,
        string plaintext,
        Encoding encoding,
        bool isLower = false
    )
    {
        return GetHashedString(_, type, plaintext, null, encoding, isLower);
    }

    /// <summary>
    /// 获取哈希之后的字符串
    /// </summary>
    /// <param name="_"></param>
    /// <param name="type">哈希类型</param>
    /// <param name="plaintext">源字符串</param>
    /// <param name="key">key</param>
    /// <param name="encoding">编码类型</param>
    /// <param name="isLower">是否是小写</param>
    /// <returns>哈希算法处理之后的字符串</returns>
    public static string GetHashedString(
        this IHashGenerator _,
        HashType type,
        string plaintext,
        string? key,
        Encoding encoding,
        bool isLower = false
    )
    {
        if (string.IsNullOrEmpty(plaintext))
            return string.Empty;

        var keyByte = string.IsNullOrEmpty(key) ? null : encoding.GetBytes(key);
        var plaintextByte = plaintext.GetBytes(encoding);

        return GetHashedString(_, type, plaintextByte, keyByte, isLower);
    }

    /// <summary>
    /// 计算字符串Hash值
    /// </summary>
    /// <param name="_"></param>
    /// <param name="type">hash类型</param>
    /// <param name="source">source</param>
    /// <param name="isLower">isLower</param>
    /// <returns>hash过的字节数组</returns>
    public static string GetHashedString(this IHashGenerator _, HashType type, byte[] source, bool isLower)
    {
        return GetHashedString(_, type, source, null, isLower);
    }

    /// <summary>
    /// 获取哈希之后的字符串
    /// </summary>
    /// <param name="_"></param>
    /// <param name="type">哈希类型</param>
    /// <param name="source">源</param>
    /// <param name="key">key</param>
    /// <param name="isLower">是否是小写</param>
    /// <returns>哈希算法处理之后的字符串</returns>
    public static string GetHashedString(
        this IHashGenerator _,
        HashType type,
        byte[]? source,
        byte[]? key = null,
        bool isLower = false
    )
    {
        if (null == source) return string.Empty;

        var hashedBytes = GetHashedBytes(_, type, source, key);
        var sbText = new StringBuilder();

        if (hashedBytes is null || !hashedBytes.Any()) return sbText.ToString();

        if (isLower)
            foreach (var b in hashedBytes)
                sbText.Append(b.ToString("x2"));
        else
            foreach (var b in hashedBytes)
                sbText.Append(b.ToString("X2"));

        return sbText.ToString();
    }

    /// <summary>
    /// 计算字符串Hash值
    /// </summary>
    /// <param name="_"></param>
    /// <param name="type">hash类型</param>
    /// <param name="plaintext">要hash的字符串</param>
    /// <returns>hash过的字节数组</returns>
    public static byte[]? GetHashedBytes(this IHashGenerator _, HashType type, string plaintext)
    {
        return GetHashedBytes(_, type, plaintext, Encoding.UTF8);
    }

    /// <summary>
    /// 计算字符串Hash值
    /// </summary>
    /// <param name="_"></param>
    /// <param name="type">hash类型</param>
    /// <param name="plaintext">要hash的字符串</param>
    /// <param name="encoding">编码类型</param>
    /// <returns>hash过的字节数组</returns>
    public static byte[]? GetHashedBytes(this IHashGenerator _, HashType type, string plaintext, Encoding encoding)
    {
        if (plaintext is null)
            throw new ArgumentNullException(nameof(plaintext));

        var bytes = encoding.GetBytes(plaintext);

        return GetHashedBytes(_, type, bytes);
    }

    /// <summary>
    /// 获取Hash后的字节数组
    /// </summary>
    /// <param name="_"></param>
    /// <param name="type">哈希类型</param>
    /// <param name="bytes">原字节数组</param>
    /// <returns></returns>
    public static byte[]? GetHashedBytes(this IHashGenerator _, HashType type, byte[]? bytes)
    {
        return GetHashedBytes(_, type, bytes, null);
    }

    /// <summary>
    /// 获取Hash后的字节数组
    /// </summary>
    /// <param name="_"></param>
    /// <param name="type">哈希类型</param>
    /// <param name="key">key</param>
    /// <param name="bytes">原字节数组</param>
    /// <returns></returns>
    public static byte[]? GetHashedBytes(this IHashGenerator _, HashType type, byte[]? bytes, byte[]? key)
    {
        if (null == bytes) return bytes;

        HashAlgorithm? algorithm = null;

        try
        {
            if (key == null)
                algorithm = type switch
                {
                    HashType.Md5 => MD5.Create(),
                    HashType.Sha1 => SHA1.Create(),
                    HashType.Sha256 => SHA256.Create(),
                    HashType.Sha384 => SHA384.Create(),
                    HashType.Sha512 => SHA512.Create(),
                    _ => MD5.Create()
                };
            else
                algorithm = type switch
                {
                    HashType.Md5 => new HMACMD5(key),
                    HashType.Sha1 => new HMACSHA1(key),
                    HashType.Sha256 => new HMACSHA256(key),
                    HashType.Sha384 => new HMACSHA384(key),
                    HashType.Sha512 => new HMACSHA512(key),
                    _ => new HMACMD5(key)
                };
            return algorithm.ComputeHash(bytes);
        }
        finally
        {
            algorithm?.Dispose();
        }
    }
}