namespace EasySoft.UtilityTools.Core.Assists;

// https://github.com/ElderJames/Floo/blob/f9746aed0010040fdf2353285f140730de6b1a20/src/Floo.App.Server/ProxyServer/Internal/ModelConventionHelper.cs#L11:27

/// <summary>
/// ModelConventionAssist
/// </summary>
public static class ModelConventionAssist
{
    /// <summary>
    /// CreateSelectors
    /// </summary>
    /// <param name="attributes"></param>
    /// <returns></returns>
    public static ICollection<SelectorModel> CreateSelectors(IList<object> attributes)
    {
        var routeProviders = new List<IRouteTemplateProvider>();

        var createSelectorForSilentRouteProviders = false;

        foreach (var attribute in attributes)
            if (attribute is IRouteTemplateProvider routeTemplateProvider)
            {
                if (IsSilentRouteAttribute(routeTemplateProvider))
                    createSelectorForSilentRouteProviders = true;
                else
                    routeProviders.Add(routeTemplateProvider);
            }

        if (routeProviders.Any(o => o is not IActionHttpMethodProvider)) createSelectorForSilentRouteProviders = false;

        var selectorModels = new List<SelectorModel>();

        if (routeProviders.Count == 0 && !createSelectorForSilentRouteProviders)
        {
            selectorModels.Add(CreateSelectorModel(null, attributes));
        }
        else
        {
            foreach (var routeProvider in routeProviders)
            {
                var filteredAttributes = new List<object>();

                foreach (var attribute in attributes)
                    if (ReferenceEquals(attribute, routeProvider))
                    {
                        filteredAttributes.Add(attribute);
                    }
                    else if (InRouteProviders(routeProviders, attribute))
                    {
                    }
                    else if (
                        routeProvider is IActionHttpMethodProvider &&
                        attribute is IActionHttpMethodProvider)
                    {
                    }
                    else
                    {
                        filteredAttributes.Add(attribute);
                    }

                selectorModels.Add(CreateSelectorModel(routeProvider, filteredAttributes));
            }

            if (createSelectorForSilentRouteProviders)
            {
                var filteredAttributes = attributes
                    .Where(attribute => !InRouteProviders(routeProviders, attribute))
                    .ToList();

                selectorModels.Add(CreateSelectorModel(null, filteredAttributes));
            }
        }

        return selectorModels;
    }

    /// <summary>
    /// InRouteProviders
    /// </summary>
    /// <param name="routeProviders"></param>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static bool InRouteProviders(List<IRouteTemplateProvider> routeProviders, object attribute)
    {
        return routeProviders.Any(rp => ReferenceEquals(rp, attribute));
    }

    /// <summary>
    /// CreateSelectorModel
    /// </summary>
    /// <param name="route"></param>
    /// <param name="attributes"></param>
    /// <returns></returns>
    public static SelectorModel CreateSelectorModel(IRouteTemplateProvider? route, IList<object> attributes)
    {
        var selectorModel = new SelectorModel();

        if (route != null) selectorModel.AttributeRouteModel = new AttributeRouteModel(route);

        AddRange(selectorModel.ActionConstraints, attributes.OfType<IActionConstraintMetadata>());

        var httpMethods = attributes
            .OfType<IActionHttpMethodProvider>()
            .SelectMany(a => a.HttpMethods)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (httpMethods.Length > 0) selectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(httpMethods));

        return selectorModel;
    }

    /// <summary>
    /// IsSilentRouteAttribute
    /// </summary>
    /// <param name="routeTemplateProvider"></param>
    /// <returns></returns>
    public static bool IsSilentRouteAttribute(IRouteTemplateProvider routeTemplateProvider)
    {
        return
            routeTemplateProvider.Template == null &&
            routeTemplateProvider.Order == null &&
            routeTemplateProvider.Name == null;
    }

    /// <summary>
    /// AddRange
    /// </summary>
    /// <param name="list"></param>
    /// <param name="items"></param>
    /// <typeparam name="T"></typeparam>
    public static void AddRange<T>(IList<T> list, IEnumerable<T> items)
    {
        foreach (var item in items) list.Add(item);
    }

    /// <summary>
    /// ParameterInfoEqualityComparer
    /// </summary>
    public class ParameterInfoEqualityComparer : IEqualityComparer<ParameterInfo>
    {
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(ParameterInfo? x, ParameterInfo? y)
        {
            return y != null && x != null && x.ParameterType == y.ParameterType;
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(ParameterInfo obj)
        {
            return obj.ParameterType.GetHashCode();
        }
    }
}