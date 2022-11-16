using System.ComponentModel;
using System.Reflection;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

public static class PropertyInfoExtension
{
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