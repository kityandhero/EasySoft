using System.Dynamic;
using System.Reflection;
using EasySoft.Core.Web.Framework.CommonAssists;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.UtilityTools.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.Web.Framework.Controllers;

public class CustomControllerBase : Controller
{
    public IActionResult GetAllActions()
    {
        var controllerFeature = new ControllerFeature();

        var applicationPartManager = ApplicationPartManagerAssist.GetApplicationPartManager();

        applicationPartManager?.PopulateFeature(controllerFeature);

        var data = Enumerable.Select(controllerFeature.Controllers, x => new
        {
            Namespace = x.Namespace,
            Controller = x.FullName,
            ModuleName = x.Module.Name,
            Actions = Enumerable.Where<MethodInfo>(x.DeclaredMethods,
                    m => m.IsPublic && !CustomAttributeExtensions.IsDefined((MemberInfo)m, typeof(NonActionAttribute)))
                .Select(y =>
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

        return this.Success(this, data);
    }

    protected ActionResult WrapperExecutiveResult(
        ExecutiveResult result
    )
    {
        if (!result.Success)
        {
            return this.Fail(result.Code);
        }

        return this.Success(this, new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }

    protected ActionResult WrapperExecutiveResult(
        ExecutiveResult<object> result
    )
    {
        return !result.Success ? this.Fail(result.Code) : this.Success(this, result.Data);
    }

    protected ActionResult WrapperExecutiveResult(
        ExecutiveResult<ExpandoObject> result
    )
    {
        return !result.Success ? this.Fail(result.Code) : this.Success(this, result.Data);
    }

    public ActionResult TestAccess()
    {
        return this.Success(this, new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }
}