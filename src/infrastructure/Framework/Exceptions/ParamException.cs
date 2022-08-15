namespace Framework.Exceptions
{
    /// <summary>
    /// 传入参数异常
    /// </summary>
    public class ParamException : Exception
    {
        #region 属性

        /// <summary>
        /// 传入参数名称
        /// </summary>
        public string Param { get; }

        /// <summary>
        /// 传入参数错误信息
        /// </summary>
        public string[] Errors { get; }

        /// <summary>
        /// 错误信息是否可向客户端显示
        /// </summary>
        public bool ShowError { get; }

        #endregion 属性

        #region 构造函数

        /// <summary>
        /// 传入参数异常
        /// </summary>
        /// <param name="param"> 传入参数名称</param>
        /// <param name="errors">传入参数错误信息</param>
        public ParamException(string param, params string[] errors)
        {
            Param = param;
            Errors = errors;
        }

        /// <summary>
        /// 传入参数异常
        /// </summary>
        /// <param name="param">    传入参数名称</param>
        /// <param name="showError">错误信息是否可向客户端显示</param>
        /// <param name="errors">   传入参数错误信息</param>
        public ParamException(string param, bool showError, params string[] errors)
        {
            Param = param;
            Errors = errors;
            ShowError = showError;
        }

        /// <summary>
        /// 传入参数异常
        /// </summary>
        /// <param name="param">    传入参数名称</param>
        /// <param name="error">    传入参数错误信息</param>
        /// <param name="showError">错误信息是否可向客户端显示</param>
        public ParamException(string param, string error, bool showError)
        {
            Param = param;
            Errors = new[] { error };
            ShowError = showError;
        }

        #endregion 构造函数
    }
}