using NUnit.Framework;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.UtilityToolsTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestGetRCode()
    {
        var value = QrCodeAssist.GetRCode(
            "比较长的一段文字,内容为：今天天气不错, 风和日丽,今天天气不错, 风和日丽,今天天气不错, 风和日丽,今天天气不错, 风和日丽,今天天气不错, 风和日丽,今天天气不错, 风和日丽,今天天气不错, 风和日丽,今天天气不错, 风和日丽,今天天气不错, 风和日丽。转换为二维码",
            10
        );

        "D:\\qrcodeTest.txt".WriteFile(value);

        Assert.Pass();
    }
}