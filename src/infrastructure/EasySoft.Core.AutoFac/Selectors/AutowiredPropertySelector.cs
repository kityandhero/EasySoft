using System.Reflection;
using Autofac.Core;
using EasySoft.Core.AutoFac.Attributes;

namespace EasySoft.Core.AutoFac.Selectors;

public class AutowiredPropertySelector : IPropertySelector
{
    public bool InjectProperty(PropertyInfo propertyInfo, object instance)
    {
        // 带有 AutowiredAttribute 特性的属性会进行属性注入
        return propertyInfo.CustomAttributes.Any(it => it.AttributeType == typeof(AutowiredAttribute));
    }
}