using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Builders;
using EasySoft.Core.Sql.Common;

namespace EasySoft.Core.Sql.Extensions;

/// <summary>
/// AdvanceSqlBuilderAllFieldsExtensions
/// </summary>
public static class AdvanceSqlBuilderAllFieldsExtensions
{
    /// <summary>
    /// AllFields
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="fragment"></param>
    /// <returns></returns>
    public static AdvanceSqlBuilder AllFields(this AdvanceSqlBuilder builder, string fragment = "")
    {
        var sql = builder.Sql;

        if (string.IsNullOrWhiteSpace(fragment))
        {
            sql += " * ";

            builder.Sql = sql;

            return builder;
        }

        sql += $"SELECT * {fragment}";

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// AllFields
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="models"></param>
    /// <returns></returns>
    public static AdvanceSqlBuilder AllFields(this AdvanceSqlBuilder builder, params IEntity[] models)
    {
        var list = new List<string>();

        models.ForEach(model =>
        {
            var tableName = TransferAssist.GetTableName(model);

            var properties = model.GetType().GetProperties();

            properties.ForEach(property =>
            {
                var customColumnMapperAttribute = Tools.GetAdvanceColumnAttribute(property, false);

                if (customColumnMapperAttribute == null) return;

                var v =
                    $"{TransferAssist.BuildIsNullFragment(property, $"{(string.IsNullOrWhiteSpace(model.GetSqlSchemaName()) ? "" : $"{model.GetSqlFieldDecorateStart()}{model.GetSqlSchemaName()}{model.GetSqlFieldDecorateEnd()}" + ".")}{model.GetSqlFieldDecorateStart()}{tableName}{model.GetSqlFieldDecorateEnd()}.{model.GetSqlFieldDecorateStart()}{customColumnMapperAttribute.Name}{model.GetSqlFieldDecorateEnd()}", true)} AS {model.GetSqlFieldDecorateStart()}{property.Name}{model.GetSqlFieldDecorateEnd()}";

                list.Add(v);
            });
        });

        var f = list.Join(",");

        var sql = builder.Sql;

        sql = "{0} {1}".FormatValue(sql, f);

        builder.Sql = sql;

        return builder;
    }
}