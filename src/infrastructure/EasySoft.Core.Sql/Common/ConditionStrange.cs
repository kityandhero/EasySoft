﻿using System.Linq.Expressions;
using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Enums;
using EasySoft.Core.Sql.Interfaces;

namespace EasySoft.Core.Sql.Common
{
    public class ConditionStrange<T> where T : new()
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

        public ConditionStrange() : this("")
        {
        }

        public ConditionStrange(string collaborationCondition)
        {
            CollaborationCondition = collaborationCondition;
        }

        private ConditionStrange<T> AppendCollaboration<T2>(
            CollaborationType collaborationType,
            ConditionStrange<T2> collaborationCondition
        ) where T2 : IEntity, new()
        {
            var transferResult = TransferStrangeAssist.TransferCondition(collaborationCondition);

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

        private ConditionStrange<T> BuildCollaboration<T2>(
            CollaborationType collaborationType,
            ConditionStrange<T2> collaborationCondition
        ) where T2 : IEntity, new()
        {
            var transferResult = TransferStrangeAssist.TransferCondition(collaborationCondition);

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

        public ConditionStrange<T> AndCollaboration<T2>(ConditionStrange<T2> collaborationCondition)
            where T2 : IEntity, new()
        {
            return this.BuildCollaboration(CollaborationType.And, collaborationCondition);
        }

        public ConditionStrange<T> AppendAndCollaboration<T2>(ConditionStrange<T2> collaborationCondition)
            where T2 : IEntity, new()
        {
            return this.AppendCollaboration(CollaborationType.And, collaborationCondition);
        }

        public ConditionStrange<T> OrCollaboration<T2>(ConditionStrange<T2> collaborationCondition)
            where T2 : IEntity, new()
        {
            return this.BuildCollaboration(CollaborationType.Or, collaborationCondition);
        }

        public ConditionStrange<T> AppendOrCollaboration<T2>(ConditionStrange<T2> collaborationCondition)
            where T2 : IEntity, new()
        {
            return this.AppendCollaboration(CollaborationType.Or, collaborationCondition);
        }

        public string TransferExpression(out Type type)
        {
            return TransferStrangeAssist.GetPropertyName(Expression, out type);
        }
    }
}