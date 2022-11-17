namespace EasySoft.UtilityToolsTest;

public class AesTests
{
    [Fact]
    public void TestCreateKey()
    {
        const string key = "qw4qwrqr";
        const string vi = "dfs";
        const string text = "abc";

        var encrypt = AesAssist.Encrypt(text, key, vi);

        var decrypt = AesAssist.Decrypt(encrypt, key, vi);

        decrypt.ShouldBeUnique(text);
    }
}