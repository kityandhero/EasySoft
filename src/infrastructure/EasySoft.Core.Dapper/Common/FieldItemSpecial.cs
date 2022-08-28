using System.Linq.Expressions;

namespace EasySoft.Core.Dapper.Common
{
    public class FieldItemSpecial<T>
    {
        public Expression<Func<T, object>> PropertyLambda { get; }
        public string ColumnAlias { get; set; }
        public bool ReplaceDBNullValue { get; set; }

        public FieldItemSpecial(Expression<Func<T, object>> propertyLambda)
        {
            PropertyLambda = propertyLambda ?? throw new Exception("propertyLambda not allow null");
            ColumnAlias = "";
            ReplaceDBNullValue = true;
        }
    }
}