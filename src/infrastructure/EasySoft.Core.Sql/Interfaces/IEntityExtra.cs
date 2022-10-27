namespace EasySoft.Core.Sql.Interfaces;

public interface IEntityExtra
{
    object GetKeyValue();

    string TransferPrimaryKeyValueToSql();

    object GetPrimaryKeyValue();

    void SetPrimaryKeyValue(object value);

    string GetTableName();

    string GetPrimaryKeyName();

    string GetSqlSchemaName();

    string GetSqlFieldDecorateStart();

    string GetSqlFieldDecorateEnd();

    string GetSqlFieldStringValueDecorateStart();

    string GetSqlFieldStringValueDecorateEnd();

    string GetSqlSchemaTableName();
}