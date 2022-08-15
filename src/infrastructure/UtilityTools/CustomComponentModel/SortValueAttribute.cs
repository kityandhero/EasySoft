namespace UtilityTools.CustomComponentModel
{
    /// <summary>
    /// 定义排序序列
    /// </summary>
    public class SortValueAttribute : System.ComponentModel.DescriptionAttribute
    {
        public int Sort { get; }

        public SortValueAttribute() : this(0)
        {
        }

        public SortValueAttribute(int sort) : base(sort.ToString())
        {
            this.Sort = sort;
        }
    }
}