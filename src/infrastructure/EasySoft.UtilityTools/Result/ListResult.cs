using System.Collections.Generic;
using EasySoft.UtilityTools.Enums;

namespace EasySoft.UtilityTools.Result
{
    public class ListResult<T> : BaseExecutiveResult

    {
        /// <summary>
        /// 页数据
        /// </summary>
        public List<T> List { get; set; }

        public int Count => (List).Count;

        public ListResult(ReturnCode returnCode, List<T> list) : this(new ReturnMessage(returnCode), list)
        {
        }

        public ListResult(ReturnMessage returnMessage, List<T> list) : base(returnMessage)
        {
            List = list;
        }

        public ListResult(ReturnMessage returnMessage) : base(returnMessage)
        {
            List = new List<T>();
        }

        public ListResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
        {
        }
    }
}