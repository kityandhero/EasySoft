﻿using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.Core.Infrastructure.Abstracts;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.Auxiliary.Controllers;

public class NLogInlayConfigController : BasicController
{
    public IActionResult Index()
    {
        var data = Tools.GetNlogDefaultConfig();

        return Content(data);
    }
}