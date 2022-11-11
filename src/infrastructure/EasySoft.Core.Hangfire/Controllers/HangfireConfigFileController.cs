using EasySoft.Core.Hangfire.ConfigAssist;
using EasySoft.Core.Hangfire.ConfigCollection;
using EasySoft.Core.Infrastructure.Controllers;
using EasySoft.UtilityTools.Core.ExtensionMethods;

namespace EasySoft.Core.Hangfire.Controllers;

public class HangfireConfigFileController : BasicController
{
    public IActionResult Index()
    {
        var data = new HangfireConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = HangfireConfigAssist.GetConfigFileInfo()
            },
            false
        );
    }
}