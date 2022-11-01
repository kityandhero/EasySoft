using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EasySoft.UtilityTools.Standard.Securities.Encryption;

/// <summary>
/// DES加密(UTF8)
/// </summary>
public static class DesAssist
{
    private static readonly byte[] Iv =
    {
        101, 22, 34, 47, 54, 67, 75, 89, 99, 104, 118, 124, 133, 146, 158, 167
    };

    /// <summary>  
    /// 加密
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string Encrypt(string plainText, string key)
    {
        return Encrypt(plainText, Encoding.UTF8.GetBytes(key));
    }

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string Encrypt(string plainText, byte[] key)
    {
        var buffer = Encoding.UTF8.GetBytes(plainText);
        var cipher = Encrypt(buffer, key);

        return Convert.ToBase64String(cipher);
    }

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static byte[] Encrypt(byte[] buffer, byte[] key)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(
                memoryStream,
                Aes.Create().CreateEncryptor(key, Iv),
                CryptoStreamMode.Write
            );

            cryptoStream.Write(buffer, 0, buffer.Length);
            cryptoStream.FlushFinalBlock();

            return memoryStream.ToArray();
        }
        catch (Exception)
        {
            throw new Exception("Encrypt data fail");
        }
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string Decrypt(string cipherText, string key)
    {
        return Decrypt(cipherText, Encoding.UTF8.GetBytes(key));
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string Decrypt(string cipherText, byte[] key)
    {
        var cipher = Convert.FromBase64String(cipherText);
        var plainText = Decrypt(cipher, key);

        return Encoding.UTF8.GetString(plainText);
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static byte[] Decrypt(byte[] buffer, byte[] key)
    {
        try
        {
            using var memoryStream = new MemoryStream();

            using var cryptoStream = new CryptoStream(
                memoryStream,
                Aes.Create().CreateDecryptor(key, Iv),
                CryptoStreamMode.Write
            );

            cryptoStream.Write(buffer, 0, buffer.Length);
            cryptoStream.FlushFinalBlock();

            return memoryStream.ToArray();
        }
        catch (Exception)
        {
            throw new Exception("Decrypt data fail");
        }
    }
}