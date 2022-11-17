using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ConfigCollection;

namespace EasySoft.Core.Config.Controllers;

public class DatabaseConfigAuxiliaryController : BasicController
{
    public IActionResult GetTemplate()
    {
        var data = new DatabaseConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = DatabaseConfigAssist.GetConfigFileInfo()
            },
            false
        );
    }

    public async Task<IActionResult> GetCurrent()
    {
        return Content(await DatabaseConfigAssist.GetConfigFileContent());
    }
}