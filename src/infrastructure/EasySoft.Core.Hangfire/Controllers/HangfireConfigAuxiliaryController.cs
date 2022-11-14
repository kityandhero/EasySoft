using EasySoft.Core.Hangfire.ConfigAssist;
using EasySoft.Core.Hangfire.ConfigCollection;

namespace EasySoft.Core.Hangfire.Controllers;

public class HangfireConfigAuxiliaryController : BasicController
{
    public IActionResult GetTemplate()
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

    public async Task<IActionResult> GetCurrent()
    {
        return Content(await HangfireConfigAssist.GetConfigFileContent());
    }
}