using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Infrastructure.Abstracts;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.Auxiliary.Controllers;

public class DevelopConfigFileController : BasicController
{
    public IActionResult Index()
    {
        var data = new DevelopConfig();

        return this.Success(data);
    }
}