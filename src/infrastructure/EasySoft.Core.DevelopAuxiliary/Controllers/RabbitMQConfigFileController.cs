using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Infrastructure.Controllers;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.DevelopAuxiliary.Controllers;

public class RabbitMQConfigFileController : BasicController
{
    public IActionResult Index()
    {
        var data = new RabbitMQConfig();

        return this.Success(data, null, false);
    }
}