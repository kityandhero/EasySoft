namespace EasySoft.Core.Sql.Common;

public static class Tools
{
    public static TableAttribute? GetTableAttribute<T>()
    {
        var type = typeof(T);

        var tableAttribute = type.GetCustomAttribute<TableAttribute>();

        if (tableAttribute == null) return null;

        if (string.IsNullOrWhiteSpace(tableAttribute.Name)) throw new Exception($"{type.Name}的特性 TableAttribute 赋值错误");

        return tableAttribute;
    }

    public static TableAttribute? GetTableAttribute<T>(T model)
    {
        if (model == null) throw new Exception("model disallow null");

        var tableAttribute = model.GetCustomAttribute<TableAttribute>();

        if (tableAttribute == null) return null;

        if (string.IsNullOrWhiteSpace(tableAttribute.Name))
            throw new Exception($"{model.GetType().Name}的特性 TableAttribute 赋值错误");

        return tableAttribute;
    }

    public static ColumnAttribute? GetColumnAttribute(
        PropertyInfo property,
        bool throwExceptionWhenNoAttribute = true
    )
    {
        var columnAttribute = property.TryGetCustomAttribute<ColumnAttribute>();

        if (columnAttribute == null)
        {
            if (!throwExceptionWhenNoAttribute) return columnAttribute;

            if (property.DeclaringType != null)
                throw new Exception(
                    $"类型{property.DeclaringType.Name}属性${property.Name}缺少CustomColumnMapperAttribute特性"
                );

            throw new Exception(
                $"属性${property.Name}缺少CustomColumnMapperAttribute特性"
            );
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(columnAttribute.Name)) return columnAttribute;

            if (property.DeclaringType != null)
                throw new Exception(
                    $"类型{property.DeclaringType.Name}属性${property.Name}的特性CustomColumnMapperAttribute赋值错误"
                );

            throw new Exception(
                $"属性${property.Name}的特性CustomColumnMapperAttribute赋值错误"
            );
        }
    }
}