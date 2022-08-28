namespace EasySoft.Core.Dapper.Interfaces
{
    public interface IEntity
    {
        object GetKeyValue();

        string TransferPrimaryKeyValueToSql();

        object GetPrimaryKeyValue();

        void SetPrimaryKeyValue(object value);

        string GetTableName();



        string GetTablePrimaryKeyName();

        string GetSqlSchemaName();

        string GetSqlFieldDecorateStart();

        string GetSqlFieldDecorateEnd();

        string GetSqlFieldStringValueDecorateStart();

        string GetSqlFieldStringValueDecorateEnd();

        string GetSqlSchemaTableName();
    }
}