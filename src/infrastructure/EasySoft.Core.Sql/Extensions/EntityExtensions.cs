using EasySoft.Core.Infrastructure.Entities.Interfaces;
using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Common;

namespace EasySoft.Core.Sql.Extensions;

/// <summary>
/// EntityExtensions
/// </summary>
public static class EntityExtensions
{
    /// <summary>
    /// GetPrimaryKeyValue
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static long GetPrimaryKeyValue<T>(this T entity) where T : IEntity
    {
        return entity.Id;
    }

    /// <summary>
    /// SetPrimaryKeyValue
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="value"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void SetPrimaryKeyValue<T>(this T entity, long value) where T : IEntity
    {
        entity.Id = value;
    }

    /// <summary>
    /// GetTableName
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string GetTableName<T>(this T entity) where T : IEntity
    {
        var advanceTableMapperAttribute = Tools.GetAdvanceTableMapperAttribute(entity.GetType());

        return advanceTableMapperAttribute == null ? entity.GetType().Name : advanceTableMapperAttribute.Name;
    }

    #region BuildNameValueList

    /// <summary>
    /// BuildNameValueList
    /// </summary>
    /// <param name="model"></param>
    /// <param name="listPropertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal static List<string> BuildNameValueList<T>(
        this T model,
        ICollection<Expression<Func<T, object>>> listPropertyLambda
    ) where T : IEntity, new()
    {
        if (listPropertyLambda == null || !listPropertyLambda.Any())
        {
            throw new Exception("缺少指定的更新属性");
        }

        var nameValueList = new List<string>();

        var listPropertyName = new List<string>();

        listPropertyLambda.ForEach(
            expression =>
            {
                var propertyName = ReflectionAssist.GetPropertyName(expression);

                listPropertyName.Add(propertyName);
            }
        );

        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        model.GetType()
            .GetProperties()
            .ForEach(
                p =>
                {
                    if (!p.CanWrite)
                    {
                        return;
                    }

                    if (!listPropertyName.Contains(p.Name))
                    {
                        return;
                    }

                    var columnName = TransferAssist.GetColumnName(p);

                    if (string.IsNullOrWhiteSpace(columnName))
                    {
                        throw new Exception("AdvanceColumnMapperAttribute is null");
                    }

                    if (!columnName.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                    {
                        nameValueList.Add(
                            $"{fieldDecorateStart}{columnName}{fieldDecorateEnd} = @{p.Name}"
                        );
                    }
                }
            );

        return nameValueList;
    }

    /// <summary>
    /// BuildNameValueList
    /// </summary>
    /// <param name="model"></param>
    /// <param name="listPropertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal static List<string> BuildNameValueList<T>(
        this T model,
        ICollection<Expression<Func<T>>> listPropertyLambda
    ) where T : IEntity, new()
    {
        if (listPropertyLambda == null || !listPropertyLambda.Any())
        {
            throw new Exception("缺少指定的更新属性");
        }

        var nameValueList = new List<string>();

        var listPropertyName = new List<string>();

        listPropertyLambda.ForEach(
            expression =>
            {
                var propertyName = ReflectionAssist.GetPropertyName(expression);

                listPropertyName.Add(propertyName);
            }
        );

        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        model.GetType()
            .GetProperties()
            .ForEach(
                p =>
                {
                    if (!p.CanWrite)
                    {
                        return;
                    }

                    if (!listPropertyName.Contains(p.Name))
                    {
                        return;
                    }

                    var columnName = TransferAssist.GetColumnName(p);

                    if (string.IsNullOrWhiteSpace(columnName))
                    {
                        throw new Exception("AdvanceColumnMapperAttribute is null");
                    }

                    if (!columnName.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                    {
                        nameValueList.Add(
                            $"{fieldDecorateStart}{columnName}{fieldDecorateEnd} = @{p.Name}"
                        );
                    }
                }
            );

        return nameValueList;
    }

    /// <summary>
    /// BuildNameValueList
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal static List<string> BuildNameValueList<T>(
        this T model
    ) where T : IEntity
    {
        var nameValueList = new List<string>();

        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        model.GetType()
            .GetProperties()
            .ForEach(
                p =>
                {
                    if (!p.CanWrite)
                    {
                        return;
                    }

                    var columnName = TransferAssist.GetColumnName(p);

                    if (string.IsNullOrWhiteSpace(columnName))
                    {
                        throw new Exception("AdvanceColumnMapperAttribute is null");
                    }

                    if (!p.Name.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                    {
                        nameValueList.Add(
                            $"{fieldDecorateStart}{columnName}{fieldDecorateEnd} = @{p.Name}"
                        );
                    }
                }
            );

        return nameValueList;
    }

    #endregion

    /// <summary>
    /// GetSqlSchemaTableName
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string GetSqlSchemaTableName<T>(this T entity) where T : IEntity
    {
        var schemaName = entity.GetSqlSchemaName();

        var tableAttribute = entity.GetType().GetCustomAttribute<TableAttribute>();

        if (tableAttribute == null)
        {
            throw new Exception(
                "缺少AdvanceTableAttribute特性"
            );
        }

        var name = tableAttribute.Name;

        return !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{name}" : name;
    }

    /// <summary>
    /// BuildNameValueList
    /// </summary>
    /// <param name="model"></param>
    /// <param name="nameList"></param>
    /// <param name="valueList"></param>  
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal static T BuildNameAndValueList<T>(
        this T model,
        out List<string> nameList,
        out List<string> valueList
    ) where T : IEntity
    {
        var names = new List<string>();
        var values = new List<string>();

        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        model.GetType()
            .GetProperties()
            .ForEach(
                p =>
                {
                    if (!p.CanWrite)
                    {
                        return;
                    }

                    var attribute = Tools.GetAdvanceColumnMapperAttribute(p);

                    if (attribute == null)
                    {
                        throw new Exception("AdvanceColumnMapperAttribute is null");
                    }

                    names.Add($"{fieldDecorateStart}{attribute.Name}{fieldDecorateEnd}");

                    values.Add($"@{p.Name}");
                }
            );

        nameList = names;
        valueList = values;

        return model;
    }
}