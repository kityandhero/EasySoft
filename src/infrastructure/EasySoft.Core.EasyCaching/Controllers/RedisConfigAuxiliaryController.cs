using EasySoft.Core.EasyCaching.ConfigAssist;
using EasySoft.Core.EasyCaching.ConfigCollection;

namespace EasySoft.Core.EasyCaching.Controllers;

public class RedisConfigAuxiliaryController : BasicController
{
    public IActionResult GetTemplate()
    {
        var data = new RedisConfig();

        return this.Success(
            data,
            new
            {
                ConfigureFileInfo = RedisConfigAssist.GetConfigFileInfo()
            },
            false
        );
    }

    public async Task<IActionResult> GetCurrent()
    {
        return Content(await RedisConfigAssist.GetConfigFileContent());
    }
}