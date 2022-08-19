// using System.Globalization;
// using Microsoft.AspNetCore.Http;
//
// namespace EasySoft.Core.GeneralLogTransmitter.Middlewares;
//
// public class GeneralLogMiddleware
// {
//     private readonly RequestDelegate _next;
//
//     public GeneralLogMiddleware(RequestDelegate next)
//     {
//         _next = next;
//     }
//
//     public async Task InvokeAsync(HttpContext context)
//     {
//         var cultureQuery = context.Request.Query["culture"];
//
//         if (!string.IsNullOrWhiteSpace(cultureQuery))
//         {
//             var culture = new CultureInfo(cultureQuery);
//
//             CultureInfo.CurrentCulture = culture;
//             CultureInfo.CurrentUICulture = culture;
//         }
//
//         // Call the next delegate/middleware in the pipeline.
//         await _next(context);
//     }
// }

