namespace EasySoft.Core.DevelopAuxiliary.Controllers;

public sealed class EnvironmentAuxiliaryController : BasicController
{
    public IActionResult ActionMap()
    {
        var controllerFeature = new ControllerFeature();

        var applicationPartManager = ApplicationPartManagerAssist.GetApplicationPartManager();

        applicationPartManager.PopulateFeature(controllerFeature);

        var data = controllerFeature.Controllers.Select(x => new
        {
            x.Namespace,
            Controller = x.FullName,
            ModuleName = x.Module.Name,
            ActionUrls = x.DeclaredMethods.Where(m => m.IsPublic && !m.IsDefined(typeof(NonActionAttribute)))
                .Select(y => $"{x.Name.RemoveEnd("Controller")}/{y.Name}"),
            ActionParameterDetails = x.DeclaredMethods
                .Where(m => m.IsPublic && !m.IsDefined(typeof(NonActionAttribute)))
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
                    })
        }).Cast<object>().ToList();

        return this.Success(data);
    }
}