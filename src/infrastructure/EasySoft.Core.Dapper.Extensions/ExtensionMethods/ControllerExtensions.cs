using System.Dynamic;
using EasySoft.Core.Dapper.Base;
using EasySoft.Core.Sql.Extensions;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Core.Results.Interfaces;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Result.Implements;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.Dapper.Extensions.ExtensionMethods;

public static class ControllerExtensions
{
    #region WrapperExecutiveResult

    public static IApiResult WrapperExecutiveResult<T>(
        this Controller controller,
        ExecutiveResult<T> result
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(result.Data?.ToObject());
    }

    public static IApiResult WrapperExecutiveResult<T>(
        this Controller controller,
        ExecutiveResult<T> result,
        Func<T?, object> translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success ? controller.Fail(result.Code) : controller.Success(translateCallback(result.Data));
    }

    public static IApiResult WrapperExecutiveResult<T, TAdditionalArgument1>(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        Func<T?, TAdditionalArgument1, object> translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1
                )
            );
    }

    public static IApiResult WrapperExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            object
        > translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1,
                    additionalArgument2
                )
            );
    }

    public static IApiResult WrapperExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            object
        > translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1,
                    additionalArgument2,
                    additionalArgument3
                )
            );
    }

    public static IApiResult WrapperExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3,
        TAdditionalArgument4
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        TAdditionalArgument4 additionalArgument4,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            TAdditionalArgument4,
            object
        > translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1,
                    additionalArgument2,
                    additionalArgument3,
                    additionalArgument4
                )
            );
    }

    public static IApiResult WrapperExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3,
        TAdditionalArgument4,
        TAdditionalArgument5
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        TAdditionalArgument4 additionalArgument4,
        TAdditionalArgument5 additionalArgument5,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            TAdditionalArgument4,
            TAdditionalArgument5,
            object
        > translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1,
                    additionalArgument2,
                    additionalArgument3,
                    additionalArgument4,
                    additionalArgument5
                )
            );
    }

    #region WrapperExecutiveResult

    public static IApiResult WrapperExecutiveResult<T>(
        this Controller controller,
        ExecutiveResult<T> result,
        Func<T?, ExpandoObject> translateCallback
    )
    {
        return !result.Success ? controller.Fail(result.Code) : controller.Success(translateCallback(result.Data));
    }

    public static IApiResult WrapperExecutiveResult<T>(
        this Controller controller,
        ExecutiveResult<T> result,
        Func<T?, ExpandoObject> translateCallback,
        Func<T?, ICollection<KeyValuePair<string, object?>>> additionalValueCallback
    )
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(result.Data)
                    .AddKeyValuePairCollection(
                        additionalValueCallback(result.Data)
                    )
            );
    }

    public static IApiResult WrapperExecutiveResult<T, TAdditionalArgument1>(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        Func<T?, TAdditionalArgument1, ExpandoObject> translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1
                )
            );
    }

    public static IApiResult WrapperExecutiveResult<T, TAdditionalArgument1>(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        Func<T?, TAdditionalArgument1, ExpandoObject> translateCallback,
        Func<T?, ICollection<KeyValuePair<string, object?>>> additionalValueCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                        result.Data,
                        additionalArgument1
                    )
                    .AddKeyValuePairCollection(
                        additionalValueCallback(result.Data)
                    )
            );
    }

    public static IApiResult WrapperExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            ExpandoObject
        > translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1,
                    additionalArgument2
                )
            );
    }

    public static IApiResult WrapperExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            ExpandoObject
        > translateCallback,
        Func<T?, ICollection<KeyValuePair<string, object?>>> additionalValueCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                        result.Data,
                        additionalArgument1,
                        additionalArgument2
                    )
                    .AddKeyValuePairCollection(
                        additionalValueCallback(result.Data)
                    )
            );
    }

    public static IApiResult WrapperExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            ExpandoObject
        > translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1,
                    additionalArgument2,
                    additionalArgument3
                )
            );
    }

    public static IApiResult WrapperExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            ExpandoObject
        > translateCallback,
        Func<T?, ICollection<KeyValuePair<string, object?>>> additionalValueCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                        result.Data,
                        additionalArgument1,
                        additionalArgument2,
                        additionalArgument3
                    )
                    .AddKeyValuePairCollection(
                        additionalValueCallback(result.Data)
                    )
            );
    }

    public static IApiResult WrapperExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3,
        TAdditionalArgument4
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        TAdditionalArgument4 additionalArgument4,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            TAdditionalArgument4,
            ExpandoObject
        > translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1,
                    additionalArgument2,
                    additionalArgument3,
                    additionalArgument4
                )
            );
    }

    public static IApiResult WrapperExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3,
        TAdditionalArgument4
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        TAdditionalArgument4 additionalArgument4,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            TAdditionalArgument4,
            ExpandoObject
        > translateCallback,
        Func<T?, ICollection<KeyValuePair<string, object?>>> additionalValueCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                        result.Data,
                        additionalArgument1,
                        additionalArgument2,
                        additionalArgument3,
                        additionalArgument4
                    )
                    .AddKeyValuePairCollection(
                        additionalValueCallback(result.Data)
                    )
            );
    }

    public static IApiResult WrapperExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3,
        TAdditionalArgument4,
        TAdditionalArgument5
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        TAdditionalArgument4 additionalArgument4,
        TAdditionalArgument5 additionalArgument5,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            TAdditionalArgument4,
            TAdditionalArgument5,
            ExpandoObject
        > translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1,
                    additionalArgument2,
                    additionalArgument3,
                    additionalArgument4,
                    additionalArgument5
                )
            );
    }

    public static IApiResult WrapperExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3,
        TAdditionalArgument4,
        TAdditionalArgument5
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        TAdditionalArgument4 additionalArgument4,
        TAdditionalArgument5 additionalArgument5,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            TAdditionalArgument4,
            TAdditionalArgument5,
            ExpandoObject
        > translateCallback,
        Func<T?, ICollection<KeyValuePair<string, object?>>> additionalValueCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                        result.Data,
                        additionalArgument1,
                        additionalArgument2,
                        additionalArgument3,
                        additionalArgument4,
                        additionalArgument5
                    )
                    .AddKeyValuePairCollection(
                        additionalValueCallback(result.Data)
                    )
            );
    }

    #endregion

    #region WrapperMongoExecutiveResult

    public static IApiResult WrapperMongoExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            ExpandoObject
        > translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1,
                    additionalArgument2
                )
            );
    }

    public static IApiResult WrapperMongoExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            ExpandoObject
        > translateCallback,
        Func<T?, ICollection<KeyValuePair<string, object?>>> additionalValueCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                        result.Data,
                        additionalArgument1,
                        additionalArgument2
                    )
                    .AddKeyValuePairCollection(
                        additionalValueCallback(result.Data)
                    )
            );
    }

    public static IApiResult WrapperMongoExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            ExpandoObject
        > translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1,
                    additionalArgument2,
                    additionalArgument3
                )
            );
    }

    public static IApiResult WrapperMongoExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            ExpandoObject
        > translateCallback,
        Func<T?, ICollection<KeyValuePair<string, object?>>> additionalValueCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                        result.Data,
                        additionalArgument1,
                        additionalArgument2,
                        additionalArgument3
                    )
                    .AddKeyValuePairCollection(
                        additionalValueCallback(result.Data)
                    )
            );
    }

    public static IApiResult WrapperMongoExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3,
        TAdditionalArgument4
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        TAdditionalArgument4 additionalArgument4,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            TAdditionalArgument4,
            ExpandoObject
        > translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1,
                    additionalArgument2,
                    additionalArgument3,
                    additionalArgument4
                )
            );
    }

    public static IApiResult WrapperMongoExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3,
        TAdditionalArgument4
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        TAdditionalArgument4 additionalArgument4,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            TAdditionalArgument4,
            ExpandoObject
        > translateCallback,
        Func<T?, ICollection<KeyValuePair<string, object?>>> additionalValueCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                        result.Data,
                        additionalArgument1,
                        additionalArgument2,
                        additionalArgument3,
                        additionalArgument4
                    )
                    .AddKeyValuePairCollection(
                        additionalValueCallback(result.Data)
                    )
            );
    }

    public static IApiResult WrapperMongoExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3,
        TAdditionalArgument4,
        TAdditionalArgument5
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        TAdditionalArgument4 additionalArgument4,
        TAdditionalArgument5 additionalArgument5,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            TAdditionalArgument4,
            TAdditionalArgument5,
            ExpandoObject
        > translateCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                    result.Data,
                    additionalArgument1,
                    additionalArgument2,
                    additionalArgument3,
                    additionalArgument4,
                    additionalArgument5
                )
            );
    }

    public static IApiResult WrapperMongoExecutiveResult<
        T,
        TAdditionalArgument1,
        TAdditionalArgument2,
        TAdditionalArgument3,
        TAdditionalArgument4,
        TAdditionalArgument5
    >(
        this Controller controller,
        ExecutiveResult<T> result,
        TAdditionalArgument1 additionalArgument1,
        TAdditionalArgument2 additionalArgument2,
        TAdditionalArgument3 additionalArgument3,
        TAdditionalArgument4 additionalArgument4,
        TAdditionalArgument5 additionalArgument5,
        Func<
            T?,
            TAdditionalArgument1,
            TAdditionalArgument2,
            TAdditionalArgument3,
            TAdditionalArgument4,
            TAdditionalArgument5,
            ExpandoObject
        > translateCallback,
        Func<T?, ICollection<KeyValuePair<string, object?>>> additionalValueCallback
    ) where T : BaseEntity<T>
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(
                translateCallback(
                        result.Data,
                        additionalArgument1,
                        additionalArgument2,
                        additionalArgument3,
                        additionalArgument4,
                        additionalArgument5
                    )
                    .AddKeyValuePairCollection(
                        additionalValueCallback(result.Data)
                    )
            );
    }

    #endregion

    #endregion
}