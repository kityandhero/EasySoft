using System;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Encryption;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using RSAExtensions;

namespace EasySoft.UtilityToolsTest;

public class RsaTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestGetRCode()
    {
        var rsa = RSA.Create();
        var privateKey = rsa.ExportPrivateKey(RSAKeyType.Pkcs1); //私钥

        // rsa.ImportRSAPublicKey();

        var v = rsa.EncryptBigData("test", RSAEncryptionPadding.Pkcs1);

        var ss = v;

        // var v = RsaAssist.Encrypt(privateKey, "test");

        Assert.Pass();
    }
}