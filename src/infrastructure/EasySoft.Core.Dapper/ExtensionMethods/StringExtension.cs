using System.Linq.Expressions;
using EasySoft.Core.Dapper.Assist;
using EasySoft.Core.Dapper.Common;
using EasySoft.Core.Dapper.Enums;
using EasySoft.Core.Dapper.Interfaces;

namespace EasySoft.Core.Dapper.ExtensionMethods
{
    public static class StringExtension
    {
        public static string AllField(this string sql, params IEntity[] models)
        {
            return SqlAssist.AllField(sql, models);
        }

        #region Field

        public static string Field<T>(
            this string sql,
            Expression<Func<T>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.Field(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string Field<T>(
            this string sql,
            FieldItem<T> fieldItem
        )
        {
            return SqlAssist.Field(sql, fieldItem);
        }

        public static string Field<T>(
            this string sql,
            Expression<Func<T, object>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.Field(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string Field<T>(
            this string sql,
            FieldItemSpecial<T> fieldItemSpecial
        )
        {
            return SqlAssist.Field(sql, fieldItemSpecial);
        }

        #endregion

        #region MinField

        public static string MinField<T>(
            this string sql,
            Expression<Func<T>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.MinField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string MinField<T>(
            this string sql,
            FieldItem<T> fieldItem
        )
        {
            return SqlAssist.MinField(sql, fieldItem);
        }

        public static string MinField<T>(
            this string sql,
            Expression<Func<T, object>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.MinField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string MinField<T>(
            this string sql,
            FieldItemSpecial<T> fieldItemSpecial
        )
        {
            return SqlAssist.MinField(sql, fieldItemSpecial);
        }

        #endregion

        #region MaxField

        public static string MaxField<T>(
            this string sql, Expression<Func<T>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.MaxField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string MaxField<T>(
            this string sql,
            FieldItem<T> fieldItem
        )
        {
            return SqlAssist.MaxField(sql, fieldItem);
        }

        public static string MaxField<T>(
            this string sql,
            Expression<Func<T, object>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.MaxField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string MaxField<T>(
            this string sql,
            FieldItemSpecial<T> fieldItemSpecial
        )
        {
            return SqlAssist.MaxField(sql, fieldItemSpecial);
        }

        #endregion

        #region SumField

        public static string SumField<T>(
            this string sql,
            Expression<Func<T>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.SumField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string SumField<T>(
            this string sql,
            FieldItem<T> fieldItem
        )
        {
            return SqlAssist.SumField(sql, fieldItem);
        }

        public static string SumField<T>(
            this string sql,
            Expression<Func<T, object>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.SumField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string SumField<T>(
            this string sql,
            FieldItemSpecial<T> fieldItemSpecial
        )
        {
            return SqlAssist.SumField(sql, fieldItemSpecial);
        }

        #endregion

        #region AppendField

        public static string AppendField<T>(
            this string sql,
            Expression<Func<T>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.AppendField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string AppendField<T>(
            this string sql,
            FieldItem<T> fieldItem
        )
        {
            return SqlAssist.AppendField(sql, fieldItem);
        }

        public static string AppendField<T>(
            this string sql,
            Expression<Func<T, object>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.AppendField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string AppendField<T>(
            this string sql,
            FieldItemSpecial<T> fieldItemSpecial
        )
        {
            return SqlAssist.AppendField(sql, fieldItemSpecial);
        }

        #endregion

        #region AppendMinField

        public static string AppendMinField<T>(
            this string sql,
            Expression<Func<T>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.AppendMinField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string AppendMinField<T>(
            this string sql,
            FieldItem<T> fieldItem
        )
        {
            return SqlAssist.AppendMinField(sql, fieldItem);
        }

        public static string AppendMinField<T>(
            this string sql,
            Expression<Func<T, object>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.AppendMinField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string AppendMinField<T>(
            this string sql,
            FieldItemSpecial<T> fieldItemSpecial
        )
        {
            return SqlAssist.AppendMinField(sql, fieldItemSpecial);
        }

        #endregion

        #region AppendMaxField

        public static string AppendMaxField<T>(
            this string sql,
            Expression<Func<T>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.AppendMaxField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string AppendMaxField<T>(
            this string sql,
            FieldItem<T> fieldItem
        )
        {
            return SqlAssist.AppendMaxField(sql, fieldItem);
        }

        public static string AppendMaxField<T>(
            this string sql,
            Expression<Func<T, object>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.AppendMaxField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string AppendMaxField<T>(
            this string sql,
            FieldItemSpecial<T> fieldItemSpecial
        )
        {
            return SqlAssist.AppendMaxField(sql, fieldItemSpecial);
        }

        #endregion

        #region AppendSumField

        public static string AppendSumField<T>(
            this string sql,
            Expression<Func<T>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.AppendSumField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string AppendSumField<T>(
            this string sql,
            FieldItem<T> fieldItem
        )
        {
            return SqlAssist.AppendSumField(sql, fieldItem);
        }

        public static string AppendSumField<T>(
            this string sql,
            Expression<Func<T, object>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return SqlAssist.AppendSumField(sql, propertyLambda, columnAlias, replaceDBNullValue);
        }

        public static string AppendSumField<T>(
            this string sql,
            FieldItemSpecial<T> fieldItemSpecial
        )
        {
            return SqlAssist.AppendSumField(sql, fieldItemSpecial);
        }

        #endregion

        #region AppendCountField

        public static string AppendCountField<T>(
            this string sql,
            Expression<Func<T>> propertyLambda,
            string columnAlias = ""
        )
        {
            return SqlAssist.AppendCountField(sql, propertyLambda, columnAlias);
        }

        public static string AppendCountField<T>(
            this string sql,
            Expression<Func<T, object>> propertyLambda,
            string columnAlias = ""
        )
        {
            return SqlAssist.AppendCountField(sql, propertyLambda, columnAlias);
        }

        #endregion

        public static string Fields<T>(
            this string sql,
            params Expression<Func<T, object>>[] propertyLambdas
        )
        {
            return SqlAssist.Fields(sql, propertyLambdas);
        }

        public static string Fields<T>(
            this string sql,
            params FieldItemSpecial<T>[] fieldItemSpecials
        )
        {
            return SqlAssist.Fields(sql, fieldItemSpecials);
        }

        #region Fields

        #endregion

        public static string AppendFragment(this string sql, string sqlFragment)
        {
            return SqlAssist.AppendFragment(sql, sqlFragment);
        }

        public static string Sum(this string sql, string sqlFragment, string valueWhenNUll)
        {
            return SqlAssist.Sum(sql, sqlFragment, valueWhenNUll);
        }

        public static string From<T>(this string sql) where T : IEntity, new()
        {
            return SqlAssist.From(sql, new T());
        }

        public static string From<T>(this string sql, T model) where T : IEntity
        {
            return SqlAssist.From(sql, model);
        }

        public static string FromInnerQuery(
            this string sql,
            string innerQuery,
            string aliasInnerQueryResult = "t"
        )
        {
            return SqlAssist.FromInnerQuery(sql, innerQuery, aliasInnerQueryResult);
        }

        public static string InnerJoin<T>(this string sql) where T : IEntity, new()
        {
            return SqlAssist.InnerJoin<T>(sql);
        }

        public static string InnerJoin<T>(this string sql, T model) where T : IEntity
        {
            return SqlAssist.InnerJoin(sql, model);
        }

        public static string LeftJoin<T>(this string sql) where T : IEntity, new()
        {
            return SqlAssist.LeftJoin<T>(sql);
        }

        public static string LeftJoin<T>(this string sql, T model) where T : IEntity
        {
            return SqlAssist.LeftJoin(sql, model);
        }

        public static string On<T1, T2>(this string sql, Expression<Func<T1>> propertyLambda,
            Expression<Func<T2>> propertyLambda2)
        {
            return SqlAssist.On(sql, propertyLambda, propertyLambda2);
        }

        public static string On<T>(this string sql, Expression<Func<T>> propertyLambda, string p2)
        {
            return SqlAssist.On(sql, propertyLambda, p2);
        }

        public static string On<T>(this string sql, Expression<Func<T>> propertyLambda, Guid p2)
        {
            return SqlAssist.On(sql, propertyLambda, p2);
        }

        public static string On<T>(this string sql, Expression<Func<T>> propertyLambda, int p2)
        {
            return SqlAssist.On(sql, propertyLambda, p2);
        }

        public static string And<T1, T2>(
            this string sql,
            Expression<Func<T1>> propertyLambda,
            Expression<Func<T2>> propertyLambda2,
            ConditionType conditionType = ConditionType.Eq
        )
        {
            return SqlAssist.And(sql, propertyLambda, propertyLambda2, conditionType);
        }

        public static string And<T>(this string sql, Expression<Func<T>> propertyLambda, string p2)
        {
            return SqlAssist.And(sql, propertyLambda, p2);
        }

        public static string And<T>(this string sql, Expression<Func<T>> propertyLambda, Guid p2)
        {
            return SqlAssist.And(sql, propertyLambda, p2);
        }

        public static string And<T>(
            this string sql,
            Expression<Func<T>> propertyLambda,
            int p2,
            ConditionType conditionType = ConditionType.Eq
        )
        {
            return SqlAssist.And(sql, propertyLambda, p2, conditionType);
        }

        public static string And<T>(
            this string sql,
            Expression<Func<T>> propertyLambda,
            long p2,
            ConditionType conditionType = ConditionType.Eq
        )
        {
            return SqlAssist.And(sql, propertyLambda, p2, conditionType);
        }

        public static string And<T>(this string sql, Condition<T> condition) where T : IEntity, new()
        {
            return SqlAssist.And(sql, condition);
        }

        #region Condition

        public static string OnlyCondition<T>(this string sql, Condition<T> condition)
            where T : IEntity, new()
        {
            return SqlAssist.OnlyCondition(sql, condition);
        }

        public static string WhereCondition<T>(this string sql, Condition<T> condition) where T : IEntity, new()
        {
            return SqlAssist.WhereCondition(sql, condition);
        }

        public static string AndCondition<T>(this string sql, Condition<T> condition) where T : IEntity, new()
        {
            return SqlAssist.AndCondition(sql, condition);
        }

        public static string LinkConditions<T>(this string sql, ICollection<Condition<T>> conditions)
            where T : IEntity, new()
        {
            return SqlAssist.LinkConditions(sql, conditions);
        }

        public static string LinkCondition<T>(this string sql, Condition<T> condition) where T : IEntity, new()
        {
            return SqlAssist.LinkCondition(sql, condition);
        }

        #endregion

        #region ConditionStrange

        public static string OnlyConditionStrange<T>(
            this string sql,
            ConditionStrange<T> condition
        ) where T : IEntity, new()
        {
            return SqlAssist.OnlyConditionStrange(sql, condition);
        }

        public static string WhereConditionStrange<T>(
            this string sql,
            ConditionStrange<T> condition
        ) where T : IEntity, new()
        {
            return SqlAssist.WhereConditionStrange(sql, condition);
        }

        public static string AndConditionStrange<T>(
            this string sql,
            ConditionStrange<T> condition
        ) where T : IEntity, new()
        {
            return SqlAssist.AndConditionStrange(sql, condition);
        }

        public static string LinkConditionStrange<T>(
            this string sql,
            ConditionStrange<T> condition
        ) where T : IEntity, new()
        {
            return SqlAssist.LinkConditionStrange(sql, condition);
        }

        #endregion

        public static string OrderByFragment(this string sql, string fragment, SortType sortType)
        {
            return SqlAssist.OrderByFragment(sql, fragment, sortType);
        }

        public static string OrderBy<T>(this string sql, Sort<T> sort)
        {
            return SqlAssist.OrderBy(sql, sort);
        }

        public static string OrderBy<T>(this string sql, Expression<Func<T, object>> propertyLambda,
            SortType sortType = SortType.Asc)
        {
            return SqlAssist.OrderBy(sql, propertyLambda, sortType);
        }

        public static string AndOrderByFragment(this string sql, string fragment, SortType sortType)
        {
            return SqlAssist.AndOrderByFragment(sql, fragment, sortType);
        }

        public static string AndOrderBy<T>(this string sql, Sort<T> sort)
        {
            return SqlAssist.AndOrderBy(sql, sort);
        }

        public static string AndOrderBy<T>(this string sql, Expression<Func<T, object>> propertyLambda,
            SortType sortType = SortType.Asc)
        {
            return SqlAssist.AndOrderBy(sql, propertyLambda, sortType);
        }

        public static string GroupBy<T>(this string sql, Group<T> group)
        {
            return SqlAssist.GroupBy(sql, group);
        }

        public static string GroupBy<T>(this string sql, Expression<Func<T, object>> propertyLambda)
        {
            return SqlAssist.GroupBy(sql, propertyLambda);
        }

        public static string AndGroupBy<T>(this string sql, Group<T> group)
        {
            return SqlAssist.AndGroupBy(sql, group);
        }

        public static string AndGroupBy<T>(this string sql, Expression<Func<T, object>> propertyLambda)
        {
            return SqlAssist.AndGroupBy(sql, propertyLambda);
        }
    }
}