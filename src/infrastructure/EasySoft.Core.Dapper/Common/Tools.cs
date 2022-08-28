using System.Reflection;
using EasySoft.UtilityTools.Standard.Attributes;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Dapper.Common
{
    public static class Tools
    {
        public static CustomTableMapperAttribute GetCustomTableMapperAttribute<T>()
        {
            var type = typeof(T);

            var customTableMapperAttribute = type.GetCustomAttribute<CustomTableMapperAttribute>();

            if (customTableMapperAttribute == null)
            {
                throw new Exception($"{type.Name}缺少CustomTableMapperAttribute特性");
            }

            if (string.IsNullOrWhiteSpace(customTableMapperAttribute.Name))
            {
                throw new Exception($"{type.Name}的特性CustomTableMapperAttribute赋值错误");
            }

            return customTableMapperAttribute;
        }

        public static CustomTableMapperAttribute GetCustomTableMapperAttribute<T>(T model)
        {
            if (model == null)
            {
                throw new Exception("model disallow null");
            }

            var customTableMapperAttribute = model.GetAttribute<CustomTableMapperAttribute>();

            if (customTableMapperAttribute == null)
            {
                throw new Exception($"{model.GetType().Name}缺少CustomTableMapperAttribute特性");
            }

            if (string.IsNullOrWhiteSpace(customTableMapperAttribute.Name))
            {
                throw new Exception($"{model.GetType().Name}的特性CustomTableMapperAttribute赋值错误");
            }

            return customTableMapperAttribute;
        }

        public static CustomColumnMapperAttribute? GetCustomColumnMapperAttribute(
            PropertyInfo property,
            bool throwExceptionWhenNoAttribute = true
        )
        {
            var customColumnMapperAttribute = property.TryGetAttribute<CustomColumnMapperAttribute>();

            if (customColumnMapperAttribute == null)
            {
                if (!throwExceptionWhenNoAttribute)
                {
                    return customColumnMapperAttribute;
                }

                if (property.DeclaringType != null)
                {
                    throw new Exception(
                        $"类型{property.DeclaringType.Name}属性${property.Name}缺少CustomColumnMapperAttribute特性"
                    );
                }

                throw new Exception(
                    $"属性${property.Name}缺少CustomColumnMapperAttribute特性"
                );
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(customColumnMapperAttribute.Name))
                {
                    return customColumnMapperAttribute;
                }

                if (property.DeclaringType != null)
                {
                    throw new Exception(
                        $"类型{property.DeclaringType.Name}属性${property.Name}的特性CustomColumnMapperAttribute赋值错误");
                }

                throw new Exception(
                    $"属性${property.Name}的特性CustomColumnMapperAttribute赋值错误");
            }
        }
    }
}