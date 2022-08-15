using Microsoft.AspNetCore.Mvc;
using EasySoft.UtilityTools.Enums;
using EasySoft.UtilityTools.Mime;

namespace EasySoft.Core.Mvc.Framework.Results
{
    /// <summary>
    /// CustomDataResult
    /// </summary>
    public class ApiResult : ActionResult
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息文本
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 主要数据
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public object? ExtraData { get; set; }

        /// <summary>
        /// application/json
        /// </summary>
        public string ContentType => MimeCollection.Json.ContentType;

        /// <summary>
        /// CustomDataResult
        /// </summary>
        /// <param name="code"></param>
        /// <param name="success"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="extraData"></param>
        public ApiResult(
            ReturnCode code = ReturnCode.Ok,
            bool success = true,
            string message = "success",
            object? data = null,
            object? extraData = null
        )
        {
            Code = code.ToInt();
            Success = success;
            Message = message;
            Data = data;
            ExtraData = extraData;
        }

        /// <summary>
        /// CustomDataResult
        /// </summary>
        /// <param name="code"></param>
        /// <param name="success"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="extraData"></param>
        public ApiResult(
            int code = 200,
            bool success = true,
            string message = "success",
            object? data = null,
            object? extraData = null
        )
        {
            Code = code;
            Success = success;
            Message = message;
            Data = data;
            ExtraData = extraData;
        }
    }
}