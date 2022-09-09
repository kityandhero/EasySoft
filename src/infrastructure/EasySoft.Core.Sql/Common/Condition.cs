using System.Linq.Expressions;
using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Enums;
using EasySoft.Core.Sql.Interfaces;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Sql.Common
{
    public class Condition<T> where T : new()
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
        /// 判断条件
        /// </summary>
        public ConditionType ConditionType { get; set; }

        /// <summary>
        /// 协同条件
        /// </summary>
        public string CollaborationCondition { get; private set; }

        public ColumnTransferMode ColumnTransferMode { get; }

        public Condition() : this(ColumnTransferMode.ContainTableName, "")
        {
        }

        public Condition(ColumnTransferMode columnTransferMode) : this(columnTransferMode, "")
        {
            ColumnTransferMode = columnTransferMode;
        }

        public Condition(ColumnTransferMode columnTransferMode, string collaborationCondition)
        {
            ColumnTransferMode = columnTransferMode;
            CollaborationCondition = collaborationCondition;
        }

        private Condition<T> AppendCollaboration<T2>(
            CollaborationType collaborationType,
            Condition<T2> collaborationCondition
        ) where T2 : IEntity, new()
        {
            var transferResult = TransferAssist.TransferCondition(collaborationCondition);

            switch (collaborationType)
            {
                case CollaborationType.And:
                    CollaborationCondition = string.IsNullOrWhiteSpace(CollaborationCondition)
                        ? transferResult
                        : $"{CollaborationCondition} AND {transferResult}";
                    break;

                case CollaborationType.Or:
                    CollaborationCondition = string.IsNullOrWhiteSpace(CollaborationCondition)
                        ? transferResult
                        : $"{CollaborationCondition} OR {transferResult}";
                    break;
            }

            return this;
        }

        private Condition<T> BuildCollaboration<T2>(
            CollaborationType collaborationType,
            Condition<T2> collaborationCondition
        ) where T2 : IEntity, new()
        {
            var transferResult = TransferAssist.TransferCondition(collaborationCondition);

            switch (collaborationType)
            {
                case CollaborationType.And:
                    CollaborationCondition = $" AND {transferResult}";
                    break;

                case CollaborationType.Or:
                    CollaborationCondition = $" OR {transferResult}";
                    break;
            }

            return this;
        }

        public Condition<T> AndCollaboration<T2>(Condition<T2> collaborationCondition) where T2 : IEntity, new()
        {
            return this.BuildCollaboration(CollaborationType.And, collaborationCondition);
        }

        public Condition<T> AppendAndCollaboration<T2>(Condition<T2> collaborationCondition) where T2 : IEntity, new()
        {
            return this.AppendCollaboration(CollaborationType.And, collaborationCondition);
        }

        public Condition<T> OrCollaboration<T2>(Condition<T2> collaborationCondition) where T2 : IEntity, new()
        {
            return this.BuildCollaboration(CollaborationType.Or, collaborationCondition);
        }

        public Condition<T> AppendOrCollaboration<T2>(Condition<T2> collaborationCondition) where T2 : IEntity, new()
        {
            return this.AppendCollaboration(CollaborationType.Or, collaborationCondition);
        }

        public string TransferExpression(out Type type)
        {
            if (this.ColumnTransferMode == ColumnTransferMode.ContainTableName)
            {
                return TransferAssist.GetTableAndColumnName(Expression, out type);
            }

            if (this.ColumnTransferMode == ColumnTransferMode.ExclusiveTableName)
            {
                return TransferAssist.GetColumnName(Expression, out type);
            }

            throw new Exception("unknown ColumnTransferMode");
        }
    }

    public static class ConditionAssist
    {
        /// <summary>
        /// 构建占位常量查询
        /// </summary>
        public static string PlaceholderCondition = " 1=1 ";

        public static string Build<T>(IEnumerable<Condition<T>> conditions) where T : IEntity, new()
        {
            var list = new List<string>();

            var enumerable = conditions as Condition<T>[] ?? conditions.ToArray();

            if (enumerable.Any())
            {
                foreach (var c in enumerable)
                {
                    list.Add(SqlAssist.LinkCondition("", c));
                }
            }

            if (list.Count == 0)
            {
                throw new Exception("没有可以构造的Sql条件");
            }

            return $" WHERE {list.Join(" AND ")}";
        }

        /// <summary>
        /// 生成条件
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string Build<T>(Condition<T> condition) where T : IEntity, new()
        {
            return Build(new[]
            {
                condition
            });
        }

        public static string TransferCondition<T>(Condition<T> condition) where T : IEntity, new()
        {
            return SqlAssist.TransferCondition(condition);
        }
    }
}