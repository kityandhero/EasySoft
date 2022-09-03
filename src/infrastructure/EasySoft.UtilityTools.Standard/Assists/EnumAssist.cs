using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using EasySoft.UtilityTools.Standard.Attributes;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Newtonsoft.Json;

namespace EasySoft.UtilityTools.Standard.Assists
{
    public static class EnumAssist
    {
        /// <summary>
        /// GetIntValues
        /// 获取枚举值对应的整形值集合
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static int[] GetIntValues(Type enumType)
        {
            var enumValueList = Enum.GetValues(enumType);

            return enumValueList.Cast<int>().OrderBy(x => x).ToArray();
        }

        public static IEnumerable<object> BuildEnumCollectionWithValueAndName<T>() where T : struct
        {
            var result = new List<object>();

            var list = GetIntValues<T>();

            foreach (var item in list)
            {
                var name = GetNameByValue(typeof(T), item);

                var o = new
                {
                    flag = item,
                    name
                };

                result.Add(o);
            }

            return result;
        }

        /// <summary>
        /// 通过枚举构建返回数据Json结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<object> BuildEnumCollectionToValueAndDescriptionData<T>() where T : struct
        {
            return BuildEnumCollectionToValueAndDescriptionDataByExclude(new List<T>());
        }

        /// <summary>
        /// 通过枚举构建返回数据Json结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="excludes">排除的项目</param>
        /// <returns></returns>
        public static IEnumerable<object> BuildEnumCollectionToValueAndDescriptionDataByExclude<T>(
            params T[] excludes
        ) where T : struct
        {
            var list = new List<T>();

            list.AddRange(excludes);

            return BuildEnumCollectionToValueAndDescriptionDataByExclude(list);
        }

        /// <summary>
        /// 通过枚举构建返回数据Json结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listExclude">排除的项目集合</param>
        /// <returns></returns>
        public static IEnumerable<object> BuildEnumCollectionToValueAndDescriptionDataByExclude<T>(
            List<T> listExclude
        ) where T : struct
        {
            return BuildEnumCollectionToValueAndDescriptionDataByMatchMode(listExclude);
        }

        public static IEnumerable<object> BuildEnumCollectionToValueAndDescriptionDataByContain<T>(
            params T[] contains
        ) where T : struct
        {
            var list = new List<T>();

            list.AddRange(contains);

            return BuildEnumCollectionToValueAndDescriptionDataByContain(list);
        }

        /// <summary>
        /// 通过枚举构建返回数据Json结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listContain">包含的项集合</param>
        /// <returns></returns>
        public static IEnumerable<object> BuildEnumCollectionToValueAndDescriptionDataByContain<T>(
            List<T> listContain
        ) where T : struct
        {
            return BuildEnumCollectionToValueAndDescriptionDataByMatchMode(listContain, MatchMode.Contain);
        }

        /// <summary>
        /// 通过枚举构建返回数据Json结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listFilter">匹配项集合</param>
        /// <param name="matchMode">匹配模式</param>
        /// <returns></returns>
        public static IEnumerable<object> BuildEnumCollectionToValueAndDescriptionDataByMatchMode<T>(
            List<T> listFilter,
            MatchMode matchMode = MatchMode.Exclude
        ) where T : struct
        {
            var result = new List<object>();

            var list = GetIntValues<T>();

            foreach (var item in list)
            {
                if (matchMode == MatchMode.Exclude)
                {
                    if (listFilter.Exists(one => Convert.ToInt32(one) == item))
                    {
                        continue;
                    }

                    var name = GetNameByValue(typeof(T), item);

                    Enum.TryParse(name, out T r);

                    var type = r.GetType();

                    var descriptionAttribute = type.GetField(name)?.GetAttribute<DescriptionAttribute>(
                        "",
                        false,
                        MemberTypes.Field
                    );

                    var availability = (int)Whether.Yes;

                    var disableAttribute = type.GetField(name)?.GetAttribute<DisableAttribute>(
                        "",
                        false,
                        MemberTypes.Field
                    );

                    if (disableAttribute != null)
                    {
                        availability = (int)Whether.No;
                    }

                    var o = new
                    {
                        flag = item.ToString(),
                        name = descriptionAttribute?.Description ?? "",
                        availability
                    };

                    result.Add(o);
                }

                if (matchMode == MatchMode.Contain)
                {
                    if (!listFilter.Exists(one => Convert.ToInt32(one) == item))
                    {
                        continue;
                    }

                    var name = GetNameByValue(typeof(T), item);

                    Enum.TryParse(name, out T r);

                    var type = r.GetType();

                    var descriptionAttribute = type.GetField(name)?.GetAttribute<DescriptionAttribute>(
                        "",
                        false,
                        MemberTypes.Field
                    );

                    var availability = (int)Whether.Yes;

                    var disableAttribute = type.GetField(name)?.GetAttribute<DisableAttribute>(
                        "",
                        false,
                        MemberTypes.Field
                    );

                    if (disableAttribute != null)
                    {
                        availability = (int)Whether.No;
                    }

                    var o = new
                    {
                        flag = item.ToString(),
                        name = descriptionAttribute?.Description ?? "",
                        availability
                    };

                    result.Add(o);
                }
            }

            return result;
        }

        /// <summary>
        /// GetIntValues
        /// 获取枚举值对应的整形值集合
        /// </summary>
        /// <returns></returns>
        public static int[] GetIntValues<T>()
        {
            return GetIntValues(typeof(T));
        }

        /// <summary>
        /// 验证键存在
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="name">要检测的键</param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static bool ExistName(
            Type enumType,
            string name,
            StringComparison comparison = StringComparison.Ordinal
        )
        {
            var result = false;

            foreach (var oneName in Enum.GetNames(enumType))
            {
                if (!oneName.Equals(name, comparison))
                {
                    continue;
                }

                result = true;

                break;
            }

            return result;
        }

        /// <summary>
        /// 验证键存在
        /// </summary>
        /// <param name="name">要检测的键</param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static bool ExistName<T>(string name, StringComparison comparison = StringComparison.Ordinal)
        {
            return ExistName(typeof(T), name, comparison);
        }

        /// <summary>
        /// 验证值存在
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="value">要检测的值</param>
        /// <returns></returns>
        public static bool ExistValue(Type enumType, int value)
        {
            var result = false;

            foreach (int oneValue in Enum.GetValues(enumType))
            {
                if (oneValue != value)
                {
                    continue;
                }

                result = true;

                break;
            }

            return result;
        }

        /// <summary>
        /// 验证值存在
        /// </summary>
        /// <param name="value">要检测的值</param>
        /// <returns></returns>
        public static bool ExistValue<T>(int value)
        {
            return ExistValue(typeof(T), value);
        }

        /// <summary>
        /// 获取键集合
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<string> GetNameList(Type enumType)
        {
            var result = new List<string>();

            foreach (var oneName in Enum.GetNames(enumType))
            {
                result.Add(oneName);
            }

            return result;
        }

        /// <summary>
        /// 获取键集合
        /// </summary>
        /// <returns></returns>
        public static List<string> GetNameList<T>()
        {
            return GetNameList(typeof(T));
        }

        /// <summary>
        /// 通过ID获取枚举中相应的Name
        /// </summary>
        /// <returns></returns>
        public static string GetNameByValue(Type enumType, int enumValue)
        {
            var result = "";
            var valueExist = false;

            var positionValue = 0;

            foreach (int oneValue in Enum.GetValues(enumType))
            {
                positionValue += 1;

                if (oneValue != enumValue)
                {
                    continue;
                }

                valueExist = true;
                break;
            }

            if (!valueExist)
            {
                return "";
            }

            var positionName = 0;
            foreach (var oneName in Enum.GetNames(enumType))
            {
                positionName += 1;

                if (positionName != positionValue)
                {
                    continue;
                }

                result = oneName;

                break;
            }

            return result;
        }

        /// <summary>
        /// 通过ID获取枚举中相应的Name
        /// </summary>
        /// <returns></returns>
        public static string GetNameByValue<T>(int enumValue)
        {
            return GetNameByValue(typeof(T), enumValue);
        }

        /// <summary>
        /// 通过Name获取枚举中相应的值
        /// </summary>
        /// <param name="enumName">枚举中的名称</param>
        /// <param name="enumType"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static int? GetValueByName(
            Type enumType,
            string enumName,
            StringComparison comparison = StringComparison.Ordinal
        )
        {
            int? result = null;
            var valueExist = false;

            var positionName = 0;
            foreach (var oneName in Enum.GetNames(enumType))
            {
                positionName += 1;
                if (oneName.Equals(enumName, comparison))
                {
                    valueExist = true;
                    break;
                }
            }

            if (!valueExist)
            {
                return null;
            }

            var positionValue = 0;

            foreach (int oneValue in Enum.GetValues(enumType))
            {
                positionValue += 1;

                if (positionName != positionValue)
                {
                    continue;
                }

                result = oneValue;

                break;
            }

            return result;
        }

        /// <summary>
        /// 通过Name获取枚举中相应的值
        /// </summary>
        /// <param name="enumName">枚举中的名称</param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static int? GetValueByName<T>(string enumName, StringComparison comparison = StringComparison.Ordinal)
        {
            return GetValueByName(typeof(T), enumName, comparison);
        }

        /// <summary>
        /// ConvertEnumToJson
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="changeKeyToLowerFirst"></param>
        /// <param name="ignoreList"></param>
        /// <returns></returns>
        public static string ConvertEnumToJson(
            Type enumType,
            bool changeKeyToLowerFirst = false,
            IEnumerable<object>? ignoreList = null
        )
        {
            var hashList = new List<Hashtable>();
            var valueList = GetIntValues(enumType);
            var ignoreValueList = new List<int>();

            if (ignoreList != null)
            {
                ignoreValueList = ignoreList.Cast<int>().OrderBy(x => x).ToList();
            }

            foreach (var v in valueList)
            {
                if (ignoreList != null)
                {
                    if (ignoreValueList.Contains(v))
                    {
                        continue;
                    }
                }

                var h = new Hashtable();
                var s = Enum.GetName(enumType, v);

                if (string.IsNullOrWhiteSpace(s))
                {
                    throw new Exception("enumType is not enum");
                }

                if (Enum.ToObject(enumType, v) is not Enum enumItem)
                {
                    throw new Exception("data is not enum");
                }

                var descriptionAttribute = enumItem.GetAttribute<DescriptionAttribute>();

                var description = descriptionAttribute?.Description ?? "无描述";

                if (changeKeyToLowerFirst)
                {
                    h["name"] = s.ToLowerFirst();
                    h["value"] = v;
                    h["internalName"] = s;
                    h["description"] = description;
                }
                else
                {
                    h["name"] = s;
                    h["value"] = v;
                    h["description"] = description;
                }

                hashList.Add(h);
            }

            return JsonConvert.SerializeObject(hashList);
        }

        /// <summary>
        /// ConvertEnumToJson
        /// </summary>
        /// <param name="changeKeyToLowerFirst"></param>
        /// <param name="ignoreList"></param>
        /// <returns></returns>
        public static string ConvertEnumToJson<T>(bool changeKeyToLowerFirst = false, IEnumerable<T>? ignoreList = null)
        {
            return ConvertEnumToJson(
                typeof(T),
                changeKeyToLowerFirst,
                ignoreList == null ? null : ignoreList.Cast<object>()
            );
        }

        /// <summary>
        /// ConvertEnumToObject
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="changeKeyToLowerFirst"></param>
        /// <param name="ignoreList"></param>
        /// <returns></returns>
        public static object ConvertEnumToObject(
            Type enumType,
            bool changeKeyToLowerFirst = false,
            IEnumerable<object>? ignoreList = null
        )
        {
            var json = ConvertEnumToJson(enumType, changeKeyToLowerFirst, ignoreList ?? new List<object>());

            var d = JsonConvert.DeserializeObject(json);

            if (d == null)
            {
                throw new Exception("can not convert enum to object");
            }

            return d;
        }

        /// <summary>
        /// ConvertEnumToObject
        /// </summary>
        /// <param name="changeKeyToLowerFirst"></param>
        /// <param name="ignoreList"></param>
        /// <returns></returns>
        public static object ConvertEnumToObject<T>(
            bool changeKeyToLowerFirst = false,
            IEnumerable<T>? ignoreList = null
        )
        {
            return ConvertEnumToObject(
                typeof(T),
                changeKeyToLowerFirst,
                ignoreList == null ? new List<object>() : ignoreList.Cast<object>()
            );
        }
    }
}