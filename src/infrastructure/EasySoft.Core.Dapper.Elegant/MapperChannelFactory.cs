﻿namespace EasySoft.Core.Dapper.Elegant;

public static class MapperChannelFactory
{
    public static IMapperChannel GetMainMapperChannel()
    {
        return new MapperChannel("Main", DatabaseConfigAssist.GetMainConnection(), RelationDatabaseType.SqlServer);
    }

    public static IMapperChannel GetMirrorMapperChannel()
    {
        return new MapperChannel("Mirror", DatabaseConfigAssist.GetMirrorConnection(),
            RelationDatabaseType.SqlServer);
    }

    public static IMapperChannel GetHistoryMapperChannel()
    {
        return new MapperChannel("Mirror", DatabaseConfigAssist.GetHistoryConnection(),
            RelationDatabaseType.SqlServer);
    }

    public static IMapperChannel GetHistoryErrorMapperChannel()
    {
        return new MapperChannel("Mirror", DatabaseConfigAssist.GetHistoryErrorConnection(),
            RelationDatabaseType.SqlServer);
    }
}