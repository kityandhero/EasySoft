using EasySoft.UtilityTools.Enums;

namespace EasySoft.UtilityTools.Result
{
    public class ExecutiveResult : ExecutiveResult<object>
    {
        public static readonly ExecutiveResult Ok = new(ReturnCode.Ok);

        public static readonly ExecutiveResult ParamError = new(ReturnCode.ParamError);

        public static readonly ExecutiveResult NoData = new(ReturnCode.NoData);

        public static readonly ExecutiveResult NoChange = new(ReturnCode.NoChange);

        public ExecutiveResult(ReturnMessage returnMessage) : base(returnMessage)
        {
        }

        public ExecutiveResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
        {
        }
    }

    /// <summary>
    /// 执行结果，用于返回方法等的执行结果判断
    /// </summary>
    public class ExecutiveResult<T> : BaseExecutiveResult where T : new()
    {
        /// <summary>
        /// 数据  
        /// </summary>
        public T Data { get; set; }

        public ExecutiveResult(ReturnMessage returnMessage) : base(returnMessage)
        {
            Data = new T();
        }

        public ExecutiveResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
        {
        }

        public ExecutiveResult ToExecutiveResult()
        {
            return new ExecutiveResult(Code);
        }
    }

    public class ExistResult<T> : ExecutiveResult<T> where T : new()
    {
        public bool Exist { get; set; }

        public ExistResult(ReturnMessage returnMessage) : base(returnMessage)
        {
            Data = new T();
            Exist = false;
        }

        public ExistResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
        {
        }
    }
}