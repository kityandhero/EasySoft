using System.Linq.Expressions;
using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Enums;
using EasySoft.Core.Sql.Interfaces;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Sql.Common
{
    public class AssignField<T> where T : new()
    {
        /// <summary>
        /// 指向表达式
        /// </summary>
        public Expression<Func<T, object>> Expression { get; set; } = null!;

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; } = null!;

        /// <summary>
        /// 赋值模式
        /// </summary>
        public AssignFieldType AssignFieldType { get; set; }

        public string TransferExpression(out Type type)
        {
            return TransferAssist.GetTableAndColumnName(Expression, out type);
        }
    }

    public static class AssignFieldAssist
    {
        public static string Build<T>(IEnumerable<AssignField<T>> assignUpdates) where T : IEntity, new()
        {
            var list = new List<string>();

            var enumerable = assignUpdates as AssignField<T>[] ?? assignUpdates.ToArray();

            if (enumerable.Any())
            {
                foreach (var c in enumerable)
                {
                    list.Add(SqlAssist.LinkAssignField("", c));
                }
            }

            if (list.Count == 0)
            {
                throw new Exception("没有可以构造的Sql条件");
            }

            return list.Join(" , ");
        }

        /// <summary>
        /// 生成条件
        /// </summary>
        /// <param name="assignField"></param>
        /// <returns></returns>
        public static string Build<T>(AssignField<T> assignField) where T : IEntity, new()
        {
            return Build(new[]
            {
                assignField
            });
        }

        public static string TransferAssignUpdate<T>(AssignField<T> assignField) where T : IEntity, new()
        {
            return SqlAssist.TransferAssignField(assignField);
        }
    }
}