﻿using EasySoft.UtilityTools.Core.Results.Interfaces;

namespace EasySoft.Simple.Single.Application.Areas.AreaTest.Controllers;

/// <summary>
/// AreaTestController
/// </summary>
// [ApiController]
// [Route("Home")]
public class AreaTestController : AreaControllerCore
{
    // [HttpGet("doTest", Name = "DoTest")]
    /// <summary>
    /// DoTest
    /// </summary>
    /// <returns></returns>
    public IApiResult DoTest()
    {
        var a = this.ParamAsync("a", 0);

        return this.Success(new
        {
            value = a
        });
    }
}