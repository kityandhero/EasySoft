using System.Dynamic;
using System.Linq.Expressions;
using EasySoft.Core.Dapper.Interfaces;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Newtonsoft.Json;

namespace EasySoft.Core.Dapper.ExtensionMethods
{
    public static class EntityExtension
    {
        /// <summary>
        /// 转换为属性首字母小写的Object
        /// </summary>
        /// <returns></returns>
        public static object? ToObject<T>(this T entity) where T : IEntity
        {
            var d = entity.ToExpandoObject();

            d.Add(new KeyValuePair<string, object?>("Key", entity.GetKeyValue()));

            var modelName = ReflectionAssist.GetClassName(entity).ToLowerFirst();

            return JsonConvert.DeserializeObject(
                JsonConvertAssist.SerializeAndKeyToLower(d).Replace(
                    "\"id\"",
                    "\"" + modelName + "Id" + "\""
                )
            );
        }

        /// <summary>
        /// 转换为指定属性的首字母小写的Object
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static object? ToSimpleObject<T>(this T entity, ICollection<Expression<Func<T, object>>> expressions)
            where T : IEntity
        {
            if (expressions.Count == 0)
            {
                return entity.ToObject();
            }

            var className = entity.GetType().Name;
            var eo = entity.ToExpandoObject(false);
            var result = new ExpandoObject();
            var propertyList = new List<string>();

            foreach (var o in expressions)
            {
                var p = ReflectionAssist.GetClassAndPropertyName(o);

                p = p.Replace(className + ".", "");

                propertyList.Add(p);
            }

            foreach (var item in eo)
            {
                if (!propertyList.Contains(item.Key))
                {
                    continue;
                }

                result.Add(item.Key.Equals("Id")
                    ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                    : item);
            }

            result.Add(new KeyValuePair<string, object?>("Key", entity.GetKeyValue()));

            return JsonConvert.DeserializeObject(JsonConvertAssist.SerializeAndKeyToLower(result));
        }

        /// <summary>
        /// 转换为指定属性的首字母小写的Object
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static object? ToSimpleObject<T>(
            this T entity,
            ICollection<Expression<Func<object>>> expressions
        ) where T : IEntity
        {
            if (expressions.Count == 0)
            {
                return entity.ToObject();
            }

            var className = entity.GetType().Name;
            var eo = entity.ToExpandoObject(false);
            var result = new ExpandoObject();
            var propertyList = new List<string>();

            foreach (var o in expressions)
            {
                var p = ReflectionAssist.GetClassAndPropertyName(o);

                p = p.Replace(className + ".", "");

                propertyList.Add(p);
            }

            foreach (var item in eo)
            {
                if (!propertyList.Contains(item.Key))
                {
                    continue;
                }

                result.Add(item.Key.Equals("Id")
                    ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                    : item);
            }

            result.Add(new KeyValuePair<string, object?>("Key", entity.GetKeyValue()));

            return JsonConvert.DeserializeObject(JsonConvertAssist.SerializeAndKeyToLower(result));
        }

        /// <summary>
        /// 转换为排除指定属性的首字母小写的Object
        /// </summary>
        /// <returns></returns>
        public static object? ToSimpleObjectIgnore<T>(
            this T entity,
            ICollection<Expression<Func<T, object>>>? expressions
        ) where T : IEntity
        {
            if (expressions == null || expressions.Count == 0)
            {
                return entity.ToObject();
            }

            var className = entity.GetType().Name;
            var eo = entity.ToExpandoObject(false);
            var result = new ExpandoObject();
            var propertyList = new List<string>();

            foreach (var o in expressions)
            {
                var p = ReflectionAssist.GetClassAndPropertyName(o);

                p = p.Replace(className + ".", "");

                propertyList.Add(p);
            }

            foreach (var item in eo)
            {
                if (propertyList.Contains(item.Key))
                {
                    continue;
                }

                result.Add(item.Key.Equals("Id")
                    ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                    : item);
            }

            result.Add(new KeyValuePair<string, object?>("Key", entity.GetKeyValue()));

            return JsonConvert.DeserializeObject(JsonConvertAssist.SerializeAndKeyToLower(result));
        }

        /// <summary>
        /// 转换为排除指定属性的首字母小写的Object
        /// </summary>
        /// <returns></returns>
        public static object? ToSimpleObjectIgnore<T>(this T entity, ICollection<Expression<Func<object>>> expressions)
            where T : IEntity
        {
            if (expressions.Count == 0)
            {
                return entity.ToObject();
            }

            var className = entity.GetType().Name;
            var eo = entity.ToExpandoObject(false);
            var result = new ExpandoObject();
            var propertyList = new List<string>();

            foreach (var o in expressions)
            {
                var p = ReflectionAssist.GetClassAndPropertyName(o);
                p = p.Replace(className + ".", "");
                propertyList.Add(p);
            }

            foreach (var item in eo)
            {
                if (propertyList.Contains(item.Key))
                {
                    continue;
                }

                result.Add(item.Key.Equals("Id")
                    ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                    : item);
            }

            result.Add(new KeyValuePair<string, object?>("Key", entity.GetKeyValue()));

            return JsonConvert.DeserializeObject(JsonConvertAssist.SerializeAndKeyToLower(result));
        }

        public static List<object> ToListObject<T>(this IEnumerable<T> list) where T : IEntity
        {
            return list.Select(o => (object)o.ToExpandoObject()).ToList();
        }

        public static List<object> ToListSimpleObject<T>(
            this IEnumerable<T> list,
            ICollection<Expression<Func<object>>> expressions
        ) where T : IEntity
        {
            return list.Select(o => o.ToSimpleObject(expressions)).ToListFilterNullable();
        }

        public static List<object> ToListSimpleObjectIgnore<T>(
            this IEnumerable<T> list,
            ICollection<Expression<Func<object>>> expressions
        ) where T : IEntity
        {
            return list.Select(o => o.ToSimpleObjectIgnore(expressions)).ToListFilterNullable();
        }
    }
}