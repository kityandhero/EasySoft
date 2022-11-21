using RSAExtensions;

namespace EasySoft.UtilityTools.Standard.Securities.Encryption;

/// <summary>
/// RSA加解密 使用OpenSSL的公钥加密/私钥解密
/// 
/// 公私钥请使用openssl生成 例如 openssl genrsa -out rsa_private_key.pem 1024,  ssh-keygen -t rsa 命令生成的公钥私钥是不行的
/// 转换公钥 openssl rsa -in rsa_private_key.pem -pubout -out rsa_public_key.pem
/// </summary>
public static class RsaAssist
{
    /// <summary>
    /// CreateRsa
    /// </summary>
    /// <returns></returns>
    public static RSA CreateRsa()
    {
        return RSA.Create();
    }

    // public static RSA CreateRsa(string privateKey, string publicKey, RSAKeyType type)
    // {
    //     var rsa = RSA.Create();
    //
    //     rsa.ImportPrivateKey(type, privateKey);
    //     rsa.ImportPublicKey(type, publicKey);
    //
    //     return rsa;
    // }

    /// <summary>
    /// CreateKey
    /// </summary>
    /// <param name="privateKey"></param>
    /// <param name="publicKey"></param>
    /// <param name="type"></param>
    /// <param name="usePemFormat"></param>
    public static void CreateKey(
        out string privateKey,
        out string publicKey,
        RSAKeyType type = RSAKeyType.Pkcs1,
        bool usePemFormat = false
    )
    {
        var rsa = RSA.Create();

        privateKey = rsa.ExportPrivateKey(type, usePemFormat);

        publicKey = rsa.ExportPublicKey(type, usePemFormat);
    }

    /// <summary>
    /// 公钥加密
    /// </summary>
    /// <param name="publicKey"></param>
    /// <param name="text"></param>
    /// <param name="type"></param>
    /// <param name="rsaEncryptionPadding"></param>
    /// <returns></returns>
    public static string Encrypt(
        string publicKey,
        string text,
        RSAKeyType type,
        RSAEncryptionPadding rsaEncryptionPadding)
    {
        var rsa = RSA.Create();

        rsa.ImportPublicKey(type, publicKey);

        return rsa.EncryptBigData(text, rsaEncryptionPadding);
    }

    /// <summary>
    /// 私钥解密
    /// </summary>
    /// <param name="privateKey"></param>
    /// <param name="text"></param>
    /// <param name="type"></param>
    /// <param name="rsaEncryptionPadding"></param>
    /// <returns></returns>
    public static string Decrypt(
        string privateKey,
        string text,
        RSAKeyType type,
        RSAEncryptionPadding rsaEncryptionPadding)
    {
        var rsa = RSA.Create();

        rsa.ImportPrivateKey(type, privateKey);

        return rsa.DecryptBigData(text, rsaEncryptionPadding);
    }

    /// <summary>
    /// 私钥签名
    /// </summary>
    /// <param name="privateKey"></param>
    /// <param name="data"></param>
    /// <param name="type"></param>
    /// <param name="rsaSignaturePadding"></param>
    /// <param name="rsaType"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string Sign(
        string privateKey,
        string data,
        RSAKeyType type,
        RSASignaturePadding rsaSignaturePadding,
        RsaType rsaType = RsaType.Rsa,
        Encoding? encoding = null
    )
    {
        var encodingAdjust = encoding ?? Encoding.UTF8;

        var dataBytes = encodingAdjust.GetBytes(data);

        var rsa = RSA.Create();

        rsa.ImportPrivateKey(type, privateKey);

        var signData = rsa.SignData(
            dataBytes,
            TransferToHashAlgorithmName(rsaType),
            rsaSignaturePadding
        );

        return Convert.ToBase64String(signData);
    }

    /// <summary>
    /// 公钥验签
    /// </summary>
    /// <param name="publicKey"></param>
    /// <param name="data"></param>
    /// <param name="sign"></param>
    /// <param name="type"></param>
    /// <param name="rsaSignaturePadding"></param>
    /// <param name="rsaType"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static bool Verify(
        string publicKey,
        string data,
        string sign,
        RSAKeyType type,
        RSASignaturePadding rsaSignaturePadding,
        RsaType rsaType = RsaType.Rsa,
        Encoding? encoding = null
    )
    {
        var encodingAdjust = encoding ?? Encoding.UTF8;

        var dataBytes = encodingAdjust.GetBytes(data);

        var signBytes = Convert.FromBase64String(sign);

        var rsa = RSA.Create();

        rsa.ImportPublicKey(type, publicKey);

        var verifyData = rsa.VerifyData(
            dataBytes,
            signBytes,
            TransferToHashAlgorithmName(rsaType),
            rsaSignaturePadding
        );

        return verifyData;
    }

    private static HashAlgorithmName TransferToHashAlgorithmName(RsaType rsaType)
    {
        return rsaType == RsaType.Rsa ? HashAlgorithmName.SHA1 : HashAlgorithmName.SHA256;
    }
}

/// <summary>
/// RSA算法类型
/// </summary>
public enum RsaType
{
    /// <summary>
    /// SHA1
    /// </summary>
    Rsa = 0,

    /// <summary>
    /// RSA2 密钥长度至少为2048
    /// SHA256
    /// </summary>
    Rsa2
}