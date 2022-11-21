namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

/// <summary>
/// PropertyInfoExtension
/// </summary>
public static class PropertyInfoExtension
{
    /// <summary>
    /// GetCustomDescription
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string GetCustomDescription(this PropertyInfo source)
    {
        var descriptionAttribute = source.GetCustomAttribute<DescriptionAttribute>();

        if (descriptionAttribute != null)
        {
            var description = descriptionAttribute.Description;

            if (!string.IsNullOrWhiteSpace(description)) return description;
        }

        return source.Name;
    }
}