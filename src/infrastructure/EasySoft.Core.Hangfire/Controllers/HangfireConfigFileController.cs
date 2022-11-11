using EasySoft.Core.Hangfire.ConfigAssist;
using EasySoft.Core.Hangfire.ConfigCollection;

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