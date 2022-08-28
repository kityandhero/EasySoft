using System.Linq.Expressions;

namespace EasySoft.Core.Dapper.Common
{
    public class FieldItem<T>
    {
        public Expression<Func<T>> PropertyLambda { get;  }
        public string ColumnAlias { get; set; }
        public bool ReplaceDBNullValue { get; set; }

        public FieldItem(Expression<Func<T>> propertyLambda)
        {
            PropertyLambda = propertyLambda ?? throw new Exception("propertyLambda not allow null");
            ColumnAlias = "";
            ReplaceDBNullValue = true;
        }
    }
}