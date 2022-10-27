using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using EasySoft.Core.Dapper.Common;
using EasySoft.Core.Dapper.Interfaces;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Interfaces;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasySoft.Core.Dapper.Base;

public abstract class BaseEntityExtra<T> : IEntityExtraSelf<T> where T : BaseEntityExtra<T>
{
    public abstract object GetKeyValue();

    public string TransferPrimaryKeyValueToSql()
    {
        var v = GetKeyValue();

        var typeCode = Type.GetTypeCode(v.GetType());

        switch (typeCode)
        {
            case TypeCode.DateTime:
                return
                    $"{GetSqlFieldStringValueDecorateStart()}{(DateTime)v:yyyy-MM-dd HH:mm:ss.fff}{GetSqlFieldStringValueDecorateEnd()}";

            case TypeCode.String:
                return $"{GetSqlFieldStringValueDecorateStart()}{(string)v}{GetSqlFieldStringValueDecorateEnd()}";

            case TypeCode.Int16:
                return v.ToString() ?? "0";

            case TypeCode.Int32:
                return v.ToString() ?? "0";

            case TypeCode.Int64:
                return v.ToString() ?? "0";

            case TypeCode.UInt16:
                return v.ToString() ?? "0";

            case TypeCode.UInt32:
                return v.ToString() ?? "0";

            case TypeCode.UInt64:
                return v.ToString() ?? "0";

            case TypeCode.Decimal:
                return v.ToString() ?? "0";

            case TypeCode.Double:
                return v.ToString() ?? "0";

            case TypeCode.Single:
                return v.ToString() ?? "0";

            default:
                throw new Exception("TransferPrimaryKeyValueToSql 缺少判断条件");
        }
    }

    public abstract object GetPrimaryKeyValue();

    public abstract void SetPrimaryKeyValue(object value);

    public string GetTableName()
    {
        var tableAttribute = Tools.GetTableAttribute(GetType());

        return tableAttribute == null ? GetType().Name : tableAttribute.Name;
    }

    public abstract Expression<Func<T, object>> GetPrimaryKeyLambda();

    public string GetPrimaryKeyName()
    {
        var lambda = GetPrimaryKeyLambda();

        var columnAttribute = Tools.GetColumnAttribute(GetPropertyInfo(lambda));

        if (columnAttribute == null) return GetPropertyName(lambda);

        if (string.IsNullOrWhiteSpace(columnAttribute.Name))
            throw new Exception($"{nameof(columnAttribute)} disallow empty value");

        return columnAttribute.Name;
    }

    public virtual string GetSqlSchemaName()
    {
        return "";
    }

    public virtual string GetSqlFieldStringValueDecorateStart()
    {
        return "'";
    }

    public virtual string GetSqlFieldStringValueDecorateEnd()
    {
        return "'";
    }

    public virtual string GetSqlFieldDecorateStart()
    {
        return "[";
    }

    public virtual string GetSqlFieldDecorateEnd()
    {
        return "]";
    }

    public string GetSqlSchemaTableName()
    {
        var schemaName = GetSqlSchemaName();

        var tableAttribute = GetType().GetCustomAttribute<TableAttribute>();

        if (tableAttribute == null)
            throw new Exception(
                "缺少CustomTableMapperAttribute特性"
            );

        var name = tableAttribute.Name;

        return !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{name}" : name;
    }

    private PropertyInfo GetPropertyInfo(Expression<Func<T, object>> propertyLambda)
    {
        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            dynamic me = propertyLambda.Body;

            if (me.Member != null)
                if (me.Member.PropertyType != null)
                {
                    var propertyInfo = me.Member as PropertyInfo;

                    if (propertyInfo == null) throw new ArgumentException("PropertyInfo is null'");

                    return propertyInfo;
                }
        }

        if (propertyLambda.Body.NodeType == ExpressionType.Convert)
        {
            if (propertyLambda.Body is not UnaryExpression cov)
                throw new ArgumentException("Cannot analyze type get name ");

            dynamic? me = cov.Operand as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                );

            if (me.Member == null) throw new ArgumentException("Cannot analyze type get name ");

            if (me.Member.PropertyType == null) throw new ArgumentException("Cannot analyze type get name ");

            var propertyInfo = me.Member as PropertyInfo;

            if (propertyInfo == null) throw new ArgumentException("PropertyInfo is null'");

            return propertyInfo;
        }

        throw new ArgumentException("Cannot analyze type get name ");
    }

    public string GetIdentificationWithModelNamePrefix(bool toLowerFirst = false)
    {
        var modelName = ReflectionAssist.GetClassName(this);

        if (toLowerFirst) modelName = modelName.ToLowerFirst();

        var identification = ReflectionAssist.GetPropertyName(GetPrimaryKeyLambda());

        return $"{modelName}{identification}";
    }

    public string GetPropertyName(Expression<Func<T, object>> expression)
    {
        return ReflectionAssist.GetPropertyName(expression);
    }

    #region Self Convert

    /// <summary>
    /// 装配修改数据
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    protected virtual ExpandoObject EmbellishObject(ExpandoObject data)
    {
        return data;
    }

    /// <summary>
    /// 装配修改数据之后
    /// </summary>
    /// <returns></returns>
    private ExpandoObject AfterEmbellishObject()
    {
        var o = EmbellishObject(this.ToExpandoObject());
        return o;
    }

    /// <summary>
    /// 转换object前处理数据
    /// </summary>
    protected virtual void BeforeToObject()
    {
    }

    private void BeforeToObjectCore()
    {
        BeforeToObject();
    }

    /// <summary>
    /// 转换为属性首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public ExpandoObject ToObject(Func<T, ExpandoObject> func)
    {
        var additionalData = func((T)this);

        return ToObject(additionalData);
    }

    /// <summary>
    /// 转换为属性首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public ExpandoObject ToObject(ExpandoObject? additionalData = null)
    {
        BeforeToObjectCore();

        var d = AfterEmbellishObject();

        if (additionalData != null) d.Add(new KeyValuePair<string, object?>("additional", additionalData));

        d.Add(new KeyValuePair<string, object?>("Key", GetKeyValue().ToString()));

        var modelName = ReflectionAssist.GetClassName(this).ToLowerFirst();

        var identification = ReflectionAssist.GetPropertyName(GetPrimaryKeyLambda());

        var json = JsonConvertAssist.SerializeAndKeyToLower(d).Replace(
            $"\"{identification.ToLowerFirst()}\"",
            $"\"{modelName}{identification}\""
        );

        var jObject = JsonConvert.DeserializeObject<JObject>(json);

        if (jObject == null) throw new Exception("DeserializeObject result is null");

        return jObject.ToExpandoObject();
    }

    public ExpandoObject ToSimpleObject(IEnumerable<Expression<Func<T, object>>> list)
    {
        var array = list.ToArray();

        return ToSimpleObject(array);
    }

    /// <summary>
    /// 转换为指定属性的首字母小写的Object
    /// </summary>
    /// <param name="exp"></param>
    /// <returns></returns>
    public ExpandoObject ToSimpleObject(params Expression<Func<T, object>>[] exp)
    {
        if (exp.Length == 0) return ToObject();

        BeforeToObjectCore();

        var className = GetType().Name;
        var eo = this.ToExpandoObject(false);
        var result = new ExpandoObject();
        var propertyList = new List<string>();

        foreach (var o in exp)
        {
            var p = ReflectionAssist.GetPropertyName(o);

            //p = p.Replace(className + ".", "");
            propertyList.Add(p);
        }

        var identification = ReflectionAssist.GetPropertyName(GetPrimaryKeyLambda());

        foreach (var item in eo)
            if (propertyList.Contains(item.Key))
                result.Add(item.Key.Equals(identification)
                    ? new KeyValuePair<string, object?>(className + $"{identification}", item.Value)
                    : item);

        result.Add(new KeyValuePair<string, object?>("Key", GetKeyValue().ToString()));

        var json = JsonConvertAssist.SerializeAndKeyToLower(result);

        var jObject = JsonConvert.DeserializeObject<JObject>(json);

        if (jObject == null) throw new Exception("DeserializeObject result is null");

        return jObject.ToExpandoObject();
    }

    /// <summary>
    /// 转换为排除指定属性的首字母小写的Object
    /// </summary>
    /// <param name="exp"></param>
    /// <returns></returns>
    public ExpandoObject ToSimpleObjectIgnore(params Expression<Func<T, object>>[] exp)
    {
        if (exp.Length == 0) return ToObject();

        BeforeToObjectCore();

        var className = GetType().Name;
        var eo = this.ToExpandoObject(false);
        var result = new ExpandoObject();
        var propertyList = new List<string>();

        foreach (var o in exp)
        {
            var p = ReflectionAssist.GetPropertyName(o);

            //p = p.Replace(className + ".", "");
            propertyList.Add(p);
        }

        var identification = ReflectionAssist.GetPropertyName(GetPrimaryKeyLambda());

        foreach (var item in eo)
            if (!propertyList.Contains(item.Key))
                result.Add(item.Key.Equals(identification)
                    ? new KeyValuePair<string, object?>(className + $"{identification}", item.Value)
                    : item);

        result.Add(new KeyValuePair<string, object?>("Key", GetKeyValue().ToString()));

        var jObject = JsonConvert.DeserializeObject<JObject>(JsonConvertAssist.SerializeAndKeyToLower(result));

        if (jObject == null) throw new Exception("DeserializeObject result is null");

        return jObject.ToExpandoObject();
    }

    /// <summary>
    /// ToJson
    /// </summary>
    /// <returns></returns>
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    /// <summary>
    /// ToObjectJson
    /// </summary>
    /// <returns></returns>
    public string ToObjectJson()
    {
        return JsonConvert.SerializeObject(ToObject());
    }

    #endregion Self Convert
}