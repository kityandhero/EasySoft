using System.Linq;

namespace UtilityTools.Assists
{
    public static class StringAssist
    {
        /// <summary>
        /// 集合中全部为空字符串
        /// </summary>
        /// <param name="list"></param>  
        /// <returns></returns>
        public static bool IsAllNullOrWhiteSpace(params string[] list)
        {
            if (list == null)
            {
                return true;
            }

            return list.Length <= 0 || list.All(string.IsNullOrWhiteSpace);
        }

        /// <summary>
        /// 集合中存在任意空字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsAnyNullOrWhiteSpace(params string[] list)
        {
            if (list == null)
            {
                return true;
            }

            return list.Length <= 0 || list.Any(string.IsNullOrWhiteSpace);
        }
    }
}