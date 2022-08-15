using System.Collections.Generic;
using System.ComponentModel;
using EasySoft.UtilityTools.Attributes;
using EasySoft.UtilityTools.Enums;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.UtilityTools.Result
{
    public class ReturnMessage
    {
        public bool Success { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public object Extra { get; set; }

        public ReturnMessage(int code, string message, bool success)
        {
            Code = code;
            Message = message;
            Success = success;
            Extra = new { };
        }

        public ReturnMessage(int code, string message, bool success, object extra)
        {
            Code = code;
            Message = message;
            Success = success;
            Extra = extra;
        }

        public ReturnMessage(ReturnCode code)
        {
            Code = (int)code;
            Message = code.GetAttribute<DescriptionAttribute>().Description;
            Success = code.GetAttribute<ReturnCodeSuccessAttribute>().Success;
            Extra = new { };
        }

        public ReturnMessage(ReturnCode code, object extra)
        {
            Code = (int)code;
            Message = code.GetAttribute<DescriptionAttribute>().Description;
            Success = code.GetAttribute<ReturnCodeSuccessAttribute>().Success;
            Extra = extra;
        }

        public ReturnMessage AppendMessage(params string[] messages)
        {
            var list = new List<string>
            {
                Message
            };

            list.AddRange(messages);

            return ToMessage(list.Join(","));
        }

        public ReturnMessage ToMessage()
        {
            return new ReturnMessage(Code, Message, Success);
        }

        public ReturnMessage ToMessage(bool success)
        {
            return new ReturnMessage(Code, Message, success);
        }

        public ReturnMessage ToMessage(string message)
        {
            return new ReturnMessage(Code, message, Success);
        }

        public ReturnMessage ToMessage(int code)
        {
            return new ReturnMessage(code, Message, Success);
        }

        /// <summary>
        /// 空结果
        /// </summary>
        public static ReturnMessage Empty => new ReturnMessage(ReturnCode.Empty);

        /// <summary>
        /// 未知
        /// </summary>
        public static ReturnMessage Unknown => new ReturnMessage(ReturnCode.Unknown);

        /// <summary>
        /// Ok
        /// </summary>
        public static ReturnMessage Ok => new ReturnMessage(ReturnCode.Ok);

        /// <summary>
        /// 签名错误
        /// </summary>
        public static ReturnMessage SignError => new ReturnMessage(ReturnCode.SignError);

        /// <summary>
        /// 访问超时
        /// </summary>
        public static ReturnMessage TimeOut => new ReturnMessage(ReturnCode.TimeOut);

        /// <summary>
        /// 参数错误
        /// </summary>
        public static ReturnMessage ParamError => new ReturnMessage(ReturnCode.ParamError);

        /// <summary>
        /// 无数据
        /// </summary>
        public static ReturnMessage NoData => new ReturnMessage(ReturnCode.NoData);

        /// <summary>
        /// 无操作反馈
        /// </summary>
        public static ReturnMessage NoChange => new ReturnMessage(ReturnCode.NoChange);

        /// <summary>
        /// 数据错误
        /// </summary>
        public static ReturnMessage DataError => new ReturnMessage(ReturnCode.DataError);

        /// <summary>
        /// Token无效
        /// </summary>
        public static ReturnMessage AuthenticationFail => new ReturnMessage(ReturnCode.AuthenticationFail);

        /// <summary>
        /// 忽略处理
        /// </summary>
        public static ReturnMessage IgnoreHandle => new ReturnMessage(ReturnCode.IgnoreHandle);

        /// <summary>
        /// 密码不匹配
        /// </summary>
        public static ReturnMessage PasswordNotMatch => new ReturnMessage(ReturnCode.PasswordNotMatch);

        /// <summary>
        /// 程序异常
        /// </summary>
        public static ReturnMessage Exception => new ReturnMessage(ReturnCode.Exception);

        /// <summary>
        /// 方法需要重载实现
        /// </summary>
        public static ReturnMessage NeedOverride => new ReturnMessage(ReturnCode.NeedOverride);
    }
}