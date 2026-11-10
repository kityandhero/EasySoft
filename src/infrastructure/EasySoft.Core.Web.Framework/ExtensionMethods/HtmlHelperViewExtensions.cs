namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class HtmlHelperViewExtensions
{
    public static IHtmlContent Action(this IHtmlHelper helper, string action, object parameters = null)
    {
        var controller = (string)helper.ViewContext.RouteData.Values["controller"]!;

        return Action(
            helper,
            action,
            controller,
            parameters
        );
    }

    public static IHtmlContent Action(
        this IHtmlHelper helper,
        string action,
        string controller,
        object parameters = null
    )
    {
        var area = (string)helper.ViewContext.RouteData.Values["area"]!;

        return Action(
            helper,
            action,
            controller,
            area,
            parameters
        );
    }

    public static IHtmlContent Action(
        this IHtmlHelper helper,
        string action,
        string controller,
        string area,
        object parameters = null
    )
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        if (controller == null)
        {
            throw new ArgumentNullException(nameof(controller));
        }

        var task = RenderActionAsync(
            helper,
            action,
            controller,
            area,
            parameters
        );

        return task.Result;
    }

    private static async Task<IHtmlContent> RenderActionAsync(
        this IHtmlHelper helper,
        string action,
        string controller,
        string area,
        object parameters = null
    )
    {
        var serviceProvider = helper.ViewContext.HttpContext.RequestServices;
        var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        var actionSelector = serviceProvider.GetRequiredService<IActionSelector>();

        // 构建 RouteData
        var routeData = new RouteData();
        foreach (var router in helper.ViewContext.RouteData.Routers)
        {
            routeData.PushState(
                router,
                null,
                null
            );
        }

        routeData.PushState(
            null,
            new RouteValueDictionary(new { controller, action, area }),
            null
        );
        routeData.PushState(
            null,
            new RouteValueDictionary(parameters ?? new { }),
            null
        );

        // 选择目标 ActionDescriptor
        var routeContext = new RouteContext(helper.ViewContext.HttpContext) { RouteData = routeData };
        var candidates = actionSelector.SelectCandidates(routeContext);
        if (candidates == null || candidates.Count == 0)
        {
            throw new Exception("candidate disallow null");
        }

        var actionDescriptor = actionSelector.SelectBestCandidate(routeContext, candidates);
        var originalHttpContext = httpContextAccessor.HttpContext;

        try
        {
            var newHttpContext = serviceProvider.GetRequiredService<IHttpContextFactory>()
                .Create(helper.ViewContext.HttpContext.Features);

            if (newHttpContext.Items.ContainsKey(typeof(IUrlHelper)))
            {
                newHttpContext.Items.Remove(typeof(IUrlHelper));
            }

            newHttpContext.Response.Body = new MemoryStream();

            if (actionDescriptor != null)
            {
                var actionContext = new ActionContext(
                    newHttpContext,
                    routeData,
                    actionDescriptor
                );

                // ← 不再设置 actionContextAccessor.ActionContext

                var invoker = serviceProvider.GetRequiredService<IActionInvokerFactory>()
                    .CreateInvoker(actionContext);

                if (invoker != null)
                {
                    await invoker.InvokeAsync();
                }
            }

            newHttpContext.Response.Body.Position = 0;

            using var reader = new StreamReader(newHttpContext.Response.Body);
            return new HtmlString(await reader.ReadToEndAsync());
        }
        catch (Exception ex)
        {
            return new HtmlString(ex.Message);
        }
        finally
        {
            httpContextAccessor.HttpContext = originalHttpContext;

            if (helper.ViewContext.HttpContext.Items.ContainsKey(typeof(IUrlHelper)))
            {
                helper.ViewContext.HttpContext.Items.Remove(typeof(IUrlHelper));
            }
        }
    }
}