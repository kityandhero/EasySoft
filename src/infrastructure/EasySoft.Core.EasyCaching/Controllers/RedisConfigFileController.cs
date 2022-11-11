using EasySoft.Core.EasyCaching.ConfigAssist;
using EasySoft.Core.EasyCaching.ConfigCollection;
using EasySoft.Core.Infrastructure.Controllers;

namespace EasySoft.Core.EasyCaching.Controllers;

public class RedisConfigFileController : BasicController
{
    public IActionResult Index()
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
}