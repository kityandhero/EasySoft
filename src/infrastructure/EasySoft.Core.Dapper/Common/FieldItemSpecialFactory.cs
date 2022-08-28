﻿using System.Linq.Expressions;

namespace EasySoft.Core.Dapper.Common
{
    public static class FieldItemSpecialFactory
    {
        public static FieldItemSpecial<T> BuildFieldItem<T>(
            Expression<Func<T, object>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return new FieldItemSpecial<T>(propertyLambda)
            {
                ColumnAlias = columnAlias,
                ReplaceDBNullValue = replaceDBNullValue
            };
        }

        public static List<FieldItemSpecial<T>> BuildFieldItems<T>(
            params Expression<Func<T, object>>[] propertyLambdas
        )
        {
            if (propertyLambdas == null || propertyLambdas.Length <= 0)
            {
                throw new Exception("propertyLambdas not allow Null Or Empty");
            }

            var result = new List<FieldItemSpecial<T>>();

            foreach (var propertyLambda in propertyLambdas)
            {
                result.Add(new FieldItemSpecial<T>(propertyLambda));
            }

            return result;
        }
    }
}