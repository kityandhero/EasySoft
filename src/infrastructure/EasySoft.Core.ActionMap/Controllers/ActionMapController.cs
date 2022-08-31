using System.Reflection;
using EasySoft.Core.Infrastructure.Abstracts;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace EasySoft.Core.ActionMap.Controllers;

public sealed class ActionMapController : ControllerCore
{
    public IActionResult Index()
    {
        var controllerFeature = new ControllerFeature();

        var applicationPartManager = ApplicationPartManagerAssist.GetApplicationPartManager();

        applicationPartManager.PopulateFeature(controllerFeature);

        var data = controllerFeature.Controllers.Select(x => new
        {
            x.Namespace,
            Controller = x.FullName,
            ModuleName = x.Module.Name,
            Actions = x.DeclaredMethods.Where(m => m.IsPublic && !m.IsDefined(typeof(NonActionAttribute)))
                .Select(y =>
                    new
                    {
                        y.Name,
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
}