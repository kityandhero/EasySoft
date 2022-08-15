using System.Collections.Generic;
using UtilityTools.Enums;

namespace UtilityTools.Result
{
    public class PageListResult<T> : ListResult<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页条目数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总数据量
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// 是否有数据
        /// </summary>
        public bool HasData { get; }

        public PageListResult(ReturnCode returnCode) : this(new ReturnMessage(returnCode))
        {
        }

        public PageListResult(ReturnMessage returnMessage, List<T> list, int pageIndex, int pageSize, long totalSize) : base(returnMessage, list)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalSize = totalSize;
            HasData = (List).Count > 0;
        }

        public PageListResult(ReturnMessage returnMessage, List<T> list, int pageIndex, int pageSize, int totalSize) : base(returnMessage, list)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalSize = totalSize;
            HasData = (List).Count > 0;
        }

        public PageListResult(ReturnMessage returnMessage) : this(returnMessage, new List<T>(), 1, 10, 0)
        {
        }

        public PageListResult(ReturnCode returnCode, List<T> list, int pageIndex, int pageSize, long totalSize) : this(new ReturnMessage(returnCode), list, pageIndex, pageSize, totalSize)
        {
        }

        public PageListResult(ReturnCode returnCode, List<T> list, int pageIndex, int pageSize, int totalSize) : this(new ReturnMessage(returnCode), list, pageIndex, pageSize, totalSize)
        {
        }
    }
}