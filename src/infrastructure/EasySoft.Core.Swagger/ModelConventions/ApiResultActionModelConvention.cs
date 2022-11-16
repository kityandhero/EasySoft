// using EasySoft.UtilityTools.Core.Attributes;
// using EasySoft.UtilityTools.Core.Results;
// using Microsoft.AspNetCore.Mvc.ApplicationModels;
//
// namespace EasySoft.Core.Swagger.ModelConventions;
//
// public class ApiResultActionModelConvention : IActionModelConvention
// {
//     public void Apply(ActionModel action)
//     {
//         var type = action.ActionMethod.ReturnType;
//
//         if (type.IsGenericType)
//         {
//             var arguments = type.GetGenericArguments();
//
//             if (arguments.Length == 1) type = arguments[0];
//         }
//
//         if (typeof(IApiResult).IsAssignableFrom(type))
//             if (!action.ActionMethod.ExistAttribute<ApiResultAttribute>())
//             {
//                 var actionAttrs = action.ActionMethod.GetCustomAttributes().Cast<object>().ToList();
//
//                 actionAttrs.Add(new ProducesResponseTypeAttribute(typeof(IApiResult), 200));
//                 actionAttrs.Add(new ProducesResponseTypeAttribute(500));
//
//                 action.Selectors.Clear();
//
//                 ModelConventionAssist.AddRange(action.Selectors, ModelConventionAssist.CreateSelectors(actionAttrs));
//             }
//     }
// }

