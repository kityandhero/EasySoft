using System.Security.Cryptography;
using EasySoft.UtilityTools.Standard.Securities.Encryption;
using RSAExtensions;
using Shouldly;
using Xunit;

namespace EasySoft.UtilityToolsTest;

public class RsaTests
{
    private const string TestPkcs1PrivateKey =
        "MIIEowIBAAKCAQEA0YYydbzGjUyqhKeMvgsi2hjsS2LINMkCb/ac7Sv/Xcd+Jm1vyaWX/tTGzmAkybP2gSaN03cta87QfbR9s796olQgWZBhgp62FCizTTOJeMOxO16g8LCsZbkBwY03w/zxvZ8v/JQJo3V8ro9cvnIO6FMnppZG3nPSZLWJYevQrHjBXNQS+SuXfWn++KweWcYW2m0GqCLuIzeXZQI2S7vsLO/doXwObFiHet4FeYa7mR61n7FyIFJu4WBk8JwSzsrvDpBMy26lzY1NRT5QKZmd9+9UkgBE1BN3tpANnu4urtsshK2+JX737VKdsJ4Ry2D8oRTGfRSIYVtx/tWhKn2atQIDAQABAoIBAEXkxFRuQrXEC2XxFowW3utuQPTs4Ohq3XtIAxVDBg7ci4J6Iz5bFRcXF7Kkc+EtPV/b4pfWO7qo5PfqC7y4dUpNUfVOqpiOJSMYaKeqN4OnlCm/6WQhK4FLdACDAUVJzcJ5peJV59k1SgBZJpEJL1vk0WdK3h06+cZKoKefqJ6/lwo2Wy4+/5ublQA4n9w4u1NBzqS265kZw8ST/y8PP8EoQEMTSNz8bkZSYCaU/dg4TWMXuUSGoEbXdjCus2SEOCe0gNVo8CYD6J1KFXOqXSQXSnogTkbdn2JgNxi5HPhDXuI0U0xxrb3OE5LWFBXIq3NPSIfJdRBwNHllUo1QAskCgYEA6kpe262ljIaJesSaca6mgCU70wtts2jDj2g/nwR9Rxk2m1Ry/H7ehhtKFA/bzeSMHA6fB/yBhxJPZVeL/Kj0jWsuq60RvjbQu5Msxn7VAJSmOdR1tP3jb8g9yRMfbPpzoneC30ibL+k9Ejke9eamsJs+lNLItagwbNqP3hKtzbMCgYEA5PBXsOH4Hx8fFignIDwDg4z3qSYvr9+CtIQI4EPSA9n+hIQfq7KhndrB7wNDgKA+IT5jO0Y4gSq14kxaZWi0fD5pX/1YCd0uo4b920mUJm8nfZn7KKLUXRWxx3lrt/gtR/1YVXlqRzDLW9IQdr1lttcJIjIbVaPpXTpEE5Qv0fcCgYBf7gedByKrwUJ1ZB6gwZQnEBfIt/s45IJ+K38xSxNIk0hKnW9fd0sBuhbA1CV5zkSYEFyJVphqh9K+efQ1hbBsXyCC5qBHNAaPZxmERNZOII9XSmjPrMz9Lg4uUIgjhQDn2utZJU91dJiu53iH0uMZmNcs9Il959m8GFmj4h4MBQKBgQCmjWteVaarDXsSJXgBbWepBp8OQaejrVlrdjr23KAnCSquwWhBAx2st38rIRa0tt88VCTg9H8bCTV1QtLov7m+QbFrkeXAl1bcxNJd/DN5mpgCOkmMEJmqdqP1WJJTLovSua7P0BrPW+I2FKbBm5wp3lrYs9dLAIo/tqncDIyVlQKBgEOuGiiO4NRkoUYxKa8kZ8Xw6YRYYEza9xJ5D7Cc0au1zmHiE6FhAikBdKtcaoQvDtiF6vR6zsnscDS+q5MtHbaTVJd/v01MSCBD27wURi/gsqHp+6EzBUsHbA+5PJpsYaxaBDPGhfgPa0JugqiYeJ1SLjTIbiHqEWgFXDtq/zNE";

    private const string TestPkcs1PublicKey =
        "MIIBCgKCAQEA0YYydbzGjUyqhKeMvgsi2hjsS2LINMkCb/ac7Sv/Xcd+Jm1vyaWX/tTGzmAkybP2gSaN03cta87QfbR9s796olQgWZBhgp62FCizTTOJeMOxO16g8LCsZbkBwY03w/zxvZ8v/JQJo3V8ro9cvnIO6FMnppZG3nPSZLWJYevQrHjBXNQS+SuXfWn++KweWcYW2m0GqCLuIzeXZQI2S7vsLO/doXwObFiHet4FeYa7mR61n7FyIFJu4WBk8JwSzsrvDpBMy26lzY1NRT5QKZmd9+9UkgBE1BN3tpANnu4urtsshK2+JX737VKdsJ4Ry2D8oRTGfRSIYVtx/tWhKn2atQIDAQAB";

    [Fact]
    public void TestCreateKey()
    {
        RsaAssist.CreateKey(out var privateKey, out var publicKey);

        privateKey.ShouldNotBe("");
        publicKey.ShouldNotBe("");
    }

    [Fact]
    public void TestEncryptAndDecrypt()
    {
        const string text = "test";

        var encrypt = RsaAssist.Encrypt(TestPkcs1PublicKey, text, RSAKeyType.Pkcs1, RSAEncryptionPadding.Pkcs1);

        var decrypt = RsaAssist.Decrypt(TestPkcs1PrivateKey, encrypt, RSAKeyType.Pkcs1, RSAEncryptionPadding.Pkcs1);

        decrypt.ShouldBe(text);
    }

    [Fact]
    public void TestSignAndVerify()
    {
        const string text = "test";

        var sign = RsaAssist.Sign(TestPkcs1PrivateKey, text, RSAKeyType.Pkcs1, RSASignaturePadding.Pkcs1);

        var verify = RsaAssist.Verify(TestPkcs1PublicKey, text, sign, RSAKeyType.Pkcs1, RSASignaturePadding.Pkcs1);

        verify.ShouldBe(true);
    }
}