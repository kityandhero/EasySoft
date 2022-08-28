using System.Linq.Expressions;
using EasySoft.Core.Dapper.Assist;
using EasySoft.Core.Dapper.Interfaces;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Dapper.Common
{
    public class Group<T>
    {
        /// <summary>
        /// 指向表达式
        /// </summary>
        public Expression<Func<T, object>> Expression { get; set; } = null!;
    }

    public static class GroupAssist
    {
        public static string Build<T>(IEnumerable<Group<T>> sorts) where T : IEntity, new()
        {
            var list = new List<string>();

            var enumerable = sorts as Group<T>[] ?? sorts.ToArray();

            if (enumerable.Any())
            {
                foreach (var s in enumerable)
                {
                    list.Add(SqlAssist.AndGroupBy("", s.Expression));
                }
            }

            if (list.Count == 0)
            {
                throw new Exception("没有可以构造的Sql排序");
            }

            return $" GROUP BY {list.Join(" , ")}";
        }

        /// <summary>
        /// 生成条件
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string Build<T>(Group<T> condition) where T : IEntity, new()
        {
            return Build(new[]
            {
                condition
            });
        }

        public static string TransferGroup<T>(Group<T> group) where T : IEntity, new()
        {
            return SqlAssist.TransferGroup(group);
        }
    }
}