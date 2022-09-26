using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EasySoft.UtilityTools.Standard.Encryption;

/// <summary>
/// RSA加解密 使用OpenSSL的公钥加密/私钥解密
/// 
/// 公私钥请使用openssl生成  ssh-keygen -t rsa 命令生成的公钥私钥是不行的
/// </summary>
public static class RsaAssist
{
    #region 使用私钥签名

    /// <summary>
    /// 使用私钥签名
    /// </summary>
    /// <param name="privateKey">private key</param>
    /// <param name="data">原始数据</param>
    /// <param name="rsaType">Rsa/Rsa2</param>
    /// <param name="encoding">encoding, if it is null, will use UTF8</param>
    /// <returns></returns>
    public static string Sign(string privateKey, string data, RsaType rsaType = RsaType.Rsa, Encoding? encoding = null)
    {
        var encodingAdjust = encoding ?? Encoding.UTF8;

        var dataBytes = encodingAdjust.GetBytes(data);

        var signatureBytes = CreateRsaProviderFromPrivateKey(privateKey)
            .SignData(
                dataBytes,
                TransferToHashAlgorithmName(rsaType),
                RSASignaturePadding.Pkcs1
            );

        return Convert.ToBase64String(signatureBytes);
    }

    #endregion

    #region 使用公钥验证签名

    /// <summary>
    /// 使用公钥验证签名
    /// </summary>
    /// <param name="publicKey">public Key</param>
    /// <param name="data">原始数据</param>
    /// <param name="sign">签名</param>
    /// <param name="rsaType">Rsa/Rsa2</param>
    /// <param name="encoding">encoding, if it is null, will use UTF8</param>
    /// <returns></returns>
    public static bool Verify(string publicKey, string data, string sign, RsaType rsaType = RsaType.Rsa,
        Encoding? encoding = null)
    {
        var encodingAdjust = encoding ?? Encoding.UTF8;

        var dataBytes = encodingAdjust.GetBytes(data);
        var signBytes = Convert.FromBase64String(sign);

        var publicKeyRsaProvider = CreateRsaProviderFromPublicKey(publicKey);

        var verify = publicKeyRsaProvider.VerifyData(
            dataBytes,
            signBytes,
            TransferToHashAlgorithmName(rsaType),
            RSASignaturePadding.Pkcs1
        );

        return verify;
    }

    #endregion

    #region 解密

    public static string Decrypt(string privateKey, string cipher)
    {
        var v = CreateRsaProviderFromPrivateKey(privateKey).Decrypt(
            Convert.FromBase64String(cipher),
            RSAEncryptionPadding.Pkcs1
        );

        return Encoding.UTF8.GetString(v);
    }

    #endregion

    #region 加密

    public static string Encrypt(string publicKey, string text)
    {
        var publicKeyRsaProvider = CreateRsaProviderFromPublicKey(publicKey);

        var v = publicKeyRsaProvider.Encrypt(
            Encoding.UTF8.GetBytes(text),
            RSAEncryptionPadding.Pkcs1
        );

        return Convert.ToBase64String(v);
    }

    #endregion

    #region 使用私钥创建RSA实例

    private static RSA CreateRsaProviderFromPrivateKey(string privateKey)
    {
        var privateKeyBits = Convert.FromBase64String(privateKey);

        var rsa = RSA.Create();
        var rsaParameters = new RSAParameters();

        using (var binaryReader = new BinaryReader(new MemoryStream(privateKeyBits)))
        {
            var twoBytes = binaryReader.ReadUInt16();

            switch (twoBytes)
            {
                case 0x8130:
                    binaryReader.ReadByte();
                    break;

                case 0x8230:
                    binaryReader.ReadInt16();
                    break;

                default:
                    throw new Exception("Unexpected value read binaryReader.ReadUInt16()");
            }

            twoBytes = binaryReader.ReadUInt16();

            if (twoBytes != 0x0102)
            {
                throw new Exception("Unexpected version");
            }

            var readByte = binaryReader.ReadByte();

            if (readByte != 0x00)
            {
                throw new Exception("Unexpected value read binaryReader.ReadByte()");
            }

            rsaParameters.Modulus = binaryReader.ReadBytes(GetIntegerSize(binaryReader));
            rsaParameters.Exponent = binaryReader.ReadBytes(GetIntegerSize(binaryReader));
            rsaParameters.D = binaryReader.ReadBytes(GetIntegerSize(binaryReader));
            rsaParameters.P = binaryReader.ReadBytes(GetIntegerSize(binaryReader));
            rsaParameters.Q = binaryReader.ReadBytes(GetIntegerSize(binaryReader));
            rsaParameters.DP = binaryReader.ReadBytes(GetIntegerSize(binaryReader));
            rsaParameters.DQ = binaryReader.ReadBytes(GetIntegerSize(binaryReader));
            rsaParameters.InverseQ = binaryReader.ReadBytes(GetIntegerSize(binaryReader));
        }

        rsa.ImportParameters(rsaParameters);

        return rsa;
    }

    #endregion

    #region 使用公钥创建RSA实例

    private static RSA CreateRsaProviderFromPublicKey(string publicKey)
    {
        // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
        byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };

        var x509Key = Convert.FromBase64String(publicKey);

        // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
        using var mem = new MemoryStream(x509Key);
        using var binaryReader = new BinaryReader(mem);

        var twoBytes = binaryReader.ReadUInt16();

        switch (twoBytes)
        {
            //data read as little endian order (actual data order for Sequence is 30 81)
            case 0x8130:
                binaryReader.ReadByte(); //advance 1 byte
                break;

            case 0x8230:
                binaryReader.ReadInt16(); //advance 2 bytes
                break;

            default:
                throw new Exception("twoBytes can not match any");
        }

        var sequence = binaryReader.ReadBytes(15); //read the Sequence OID

        if (!CompareBytearrays(sequence, seqOid)) //make sure Sequence for OID is correct
        {
            throw new Exception("compare error");
        }

        twoBytes = binaryReader.ReadUInt16();

        switch (twoBytes)
        {
            //data read as little endian order (actual data order for Bit String is 03 81)
            case 0x8103:
                binaryReader.ReadByte(); //advance 1 byte
                break;

            case 0x8203:
                binaryReader.ReadInt16(); //advance 2 bytes
                break;

            default:
                throw new Exception("twoBytes can not match any");
        }

        var readByte = binaryReader.ReadByte();

        if (readByte != 0x00) //expect null byte next
        {
            throw new Exception("readByte error");
        }

        twoBytes = binaryReader.ReadUInt16();

        switch (twoBytes)
        {
            //data read as little endian order (actual data order for Sequence is 30 81)
            case 0x8130:
                binaryReader.ReadByte(); //advance 1 byte
                break;

            case 0x8230:
                binaryReader.ReadInt16(); //advance 2 bytes
                break;

            default:
                throw new Exception("twoBytes can not match any");
        }

        twoBytes = binaryReader.ReadUInt16();

        byte lowByte;
        byte highByte = 0x00;

        switch (twoBytes)
        {
            //data read as little endian order (actual data order for Integer is 02 81)
            case 0x8102:
                lowByte = binaryReader.ReadByte(); // read next bytes which is bytes in modulus
                break;

            case 0x8202:
                highByte = binaryReader.ReadByte(); //advance 2 bytes
                lowByte = binaryReader.ReadByte();
                break;

            default:
                throw new Exception("twoBytes can not match any");
        }

        byte[] modInt =
        {
            lowByte, highByte, 0x00, 0x00
        }; //reverse byte order since asn.1 key uses big endian order
        var modSize = BitConverter.ToInt32(modInt, 0);

        var firstByte = binaryReader.PeekChar();
        if (firstByte == 0x00)
        {
            //if first byte (highest order) of modulus is zero, don't include it
            binaryReader.ReadByte(); //skip this null byte
            modSize -= 1; //reduce modulus buffer size by 1
        }

        var modulus = binaryReader.ReadBytes(modSize); //read the modulus bytes

        if (binaryReader.ReadByte() != 0x02) //expect an Integer for the exponent data
        {
            throw new Exception("binaryReader error");
        }

        // should only need one byte for actual exponent data (for all useful values)
        var expBytes = (int)binaryReader.ReadByte();
        var exponent = binaryReader.ReadBytes(expBytes);

        // ------- create RSACryptoServiceProvider instance and initialize with public key -----
        var rsa = RSA.Create();

        var rsaKeyInfo = new RSAParameters
        {
            Modulus = modulus,
            Exponent = exponent
        };

        rsa.ImportParameters(rsaKeyInfo);

        return rsa;
    }

    #endregion

    #region 导入密钥算法

    private static int GetIntegerSize(BinaryReader binaryReader)
    {
        int count;
        var bt = binaryReader.ReadByte();

        if (bt != 0x02)
        {
            return 0;
        }

        bt = binaryReader.ReadByte();

        switch (bt)
        {
            case 0x81:
                count = binaryReader.ReadByte();
                break;

            case 0x82:
            {
                var highByte = binaryReader.ReadByte();
                var lowByte = binaryReader.ReadByte();
                byte[] modInt = { lowByte, highByte, 0x00, 0x00 };

                count = BitConverter.ToInt32(modInt, 0);

                break;
            }

            default:
                count = bt;
                break;
        }

        while (binaryReader.ReadByte() == 0x00)
        {
            count -= 1;
        }

        binaryReader.BaseStream.Seek(-1, SeekOrigin.Current);

        return count;
    }

    private static bool CompareBytearrays(IReadOnlyCollection<byte> a, IReadOnlyList<byte> b)
    {
        if (a.Count != b.Count)
        {
            return false;
        }

        var i = 0;

        foreach (var c in a)
        {
            if (c != b[i])
            {
                return false;
            }

            i++;
        }

        return true;
    }

    private static HashAlgorithmName TransferToHashAlgorithmName(RsaType rsaType)
    {
        return rsaType == RsaType.Rsa ? HashAlgorithmName.SHA1 : HashAlgorithmName.SHA256;
    }

    #endregion
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