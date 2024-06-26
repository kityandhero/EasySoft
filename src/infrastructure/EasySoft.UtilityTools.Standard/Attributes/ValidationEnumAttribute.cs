﻿using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// ValidationEnumAttribute
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class ValidationEnumAttribute : Attribute
{
    private Type EnumType { get; }

    private Array ExcludeArray { get; }

    private List<int> ArrayValue { get; }

    private int IgnoreValue { get; }

    private bool ExistIgnoreValue { get; }

    /// <summary>
    /// ValidationEnumAttribute
    /// </summary>
    /// <param name="enumType"></param>
    public ValidationEnumAttribute(Type enumType) : this(
        enumType,
        0,
        false,
        Array.Empty<object>()
    )
    {
    }

    /// <summary>
    /// ValidationEnumAttribute
    /// </summary>
    /// <param name="enumType"></param>
    /// <param name="ignoreValue"></param>
    public ValidationEnumAttribute(Type enumType, int ignoreValue) : this(
        enumType,
        ignoreValue,
        true,
        Array.Empty<object>()
    )
    {
    }

    /// <summary>
    /// ValidationEnumAttribute
    /// </summary>
    /// <param name="enumType"></param>
    /// <param name="excludeArray"></param>
    public ValidationEnumAttribute(Type enumType, params object[] excludeArray) : this(
        enumType,
        0,
        false,
        excludeArray
    )
    {
    }

    private ValidationEnumAttribute(
        Type enumType,
        int ignoreValue,
        bool existIgnoreValue,
        params object[] excludeArray
    )
    {
        EnumType = enumType;
        IgnoreValue = ignoreValue;
        ExistIgnoreValue = existIgnoreValue;
        ExcludeArray = excludeArray;
        ArrayValue = new List<int>();

        if (EnumType == null) throw new Exception($"ValidationEnumAttribute:enumType not allow null");

        if (!EnumType.IsEnum) throw new Exception($"ValidationEnumAttribute:enumType must be an Enum Type");

        var valueList = Enum.GetValues(EnumType);

        var l = new ArrayList(valueList);

        foreach (var one in l) ArrayValue.Add(Convert.ToInt32(one));

        if (ExcludeArray.Length <= 0) return;

        var list = new ArrayList(ExcludeArray);

        foreach (var item in list)
        {
            if (item == null)
                throw new Exception(
                    $"ValidationEnumAttribute:excludeArray value not allow null"
                );

            if (!item.GetType().IsEnum)
                throw new Exception(
                    $"ValidationEnumAttribute:excludeArray value must be an Enum Type"
                );

            if (item.GetType() != EnumType)
                throw new Exception(
                    $"ValidationEnumAttribute:excludeArray value type must be equal enumType"
                );

            ArrayValue.Remove(Convert.ToInt32(item));
        }
    }

    /// <summary>
    /// Validation
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public ExecutiveResult Validation(int value)
    {
        if (ExistIgnoreValue)
            if (IgnoreValue == value)
                return new ExecutiveResult(ReturnCode.Ok);

        if (!ArrayValue.Contains(value))
            return new ExecutiveResult(
                ReturnCode.Exception.ToMessage("值不在许可的范围之内")
            );

        return new ExecutiveResult(ReturnCode.Ok);
    }
}