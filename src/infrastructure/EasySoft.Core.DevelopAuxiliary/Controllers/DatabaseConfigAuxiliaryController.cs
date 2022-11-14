using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.DevelopAuxiliary.Controllers;

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