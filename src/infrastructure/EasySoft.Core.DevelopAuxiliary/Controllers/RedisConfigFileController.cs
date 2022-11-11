using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Infrastructure.Controllers;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.DevelopAuxiliary.Controllers;

public class RedisConfigFileController : BasicController
{
    public IActionResult Index()
    {
        var data = new RedisConfig();

        return this.Success(data, null, false);
    }
}