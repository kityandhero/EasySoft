namespace EasySoft.Core.Sql.Common
{
    public static class FieldItemFactory
    {
        public static FieldItem<T> BuildFieldItem<T>(
            Expression<Func<T>> propertyLambda,
            string columnAlias = "",
            bool replaceDBNullValue = true
        )
        {
            return new FieldItem<T>(propertyLambda)
            {
                ColumnAlias = columnAlias,
                ReplaceDBNullValue = replaceDBNullValue
            };
        }

        public static List<FieldItem<T>> BuildFieldItems<T>(
            params Expression<Func<T>>[] propertyLambdas
        )
        {
            if (propertyLambdas == null || propertyLambdas.Length <= 0)
            {
                throw new Exception("propertyLambdas not allow Null Or Empty");
            }
        
            var result = new List<FieldItem<T>>();
        
            foreach (var propertyLambda in propertyLambdas)
            {
                result.Add(new FieldItem<T>(propertyLambda));
            }
        
            return result;
        }
    }
}