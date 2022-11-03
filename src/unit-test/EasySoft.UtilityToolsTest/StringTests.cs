using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Shouldly;
using Xunit;

namespace EasySoft.UtilityToolsTest;

public class StringTests
{
    [Fact]
    public void TestToCamelCase()
    {
        var name = "LoginName";

        var result = name.ToCamelCase();

        result.ShouldBe("loginName");
    }

    [Fact]
    public void TestToSnakeCase()
    {
        var name = "LoginName";

        var result = name.ToSnakeCase();

        result.ShouldBe("login_name");
    }
}