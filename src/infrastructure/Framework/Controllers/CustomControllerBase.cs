﻿using System.Dynamic;
using System.Reflection;
using Framework.CommonAssists;
using Framework.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using UtilityTools.ExtensionMethods;
using UtilityTools.Result;

namespace Framework.Controllers;

public class CustomControllerBase : ControllerBase
{
    public IActionResult GetAllActions()
    {
        var controllerFeature = new ControllerFeature();

        var applicationPartManager = ApplicationPartManagerAssist.GetApplicationPartManager();

        applicationPartManager?.PopulateFeature(controllerFeature);

        var data = controllerFeature.Controllers.Select(x => new
        {
            Namespace = x.Namespace,
            Controller = x.FullName,
            ModuleName = x.Module.Name,
            Actions = x.DeclaredMethods.Where(m => m.IsPublic && !m.IsDefined(typeof(NonActionAttribute))).Select(y =>
                new
                {
                    Name = y.Name,
                    ParameterCount = y.GetParameters().Length,
                    Parameters = y.GetParameters()
                        .Select(z => new
                        {
                            z.Name,
                            z.ParameterType.FullName,
                            z.Position
                        })
                }),
        }).Cast<object>().ToList();

        return this.Success(data);
    }

    protected ActionResult WrapperExecutiveResult(
        ExecutiveResult result
    )
    {
        if (!result.Success)
        {
            return this.Fail(result.Code);
        }

        return this.Success(new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }

    protected ActionResult WrapperExecutiveResult(
        ExecutiveResult<object> result
    )
    {
        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data);
    }

    protected ActionResult WrapperExecutiveResult(
        ExecutiveResult<ExpandoObject> result
    )
    {
        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data);
    }

    public ActionResult TestAccess()
    {
        return this.Success(new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }
}