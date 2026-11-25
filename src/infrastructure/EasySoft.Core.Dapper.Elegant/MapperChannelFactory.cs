using EasySoft.UtilityTools.Core.Assists;

namespace EasySoft.Core.Dapper.Elegant;

public static class MapperChannelFactory
{
    public static IMapperChannel GetMasterMapperChannel()
    {
        var applicationConfigureAssist = ServiceProviderAssist.GetProvider()
            .GetRequiredService<ApplicationConfigureAssist>();

        return GetMasterMapperChannel(applicationConfigureAssist);
    }

    public static IMapperChannel GetMasterMapperChannel(
        ApplicationConfigureAssist applicationConfigureAssist
    )
    {
        return new MapperChannel(
            "Master",
            applicationConfigureAssist.GetMasterDatabaseConnection().Value,
            RelationDatabaseTypeAssist.GetRelationDatabaseType(
                applicationConfigureAssist.GetMasterDatabaseType().Value
            )
        );
    }

    public static IMapperChannel GetSlaveMapperChannel()
    {
        var applicationConfigureAssist = ServiceProviderAssist.GetProvider()
            .GetRequiredService<ApplicationConfigureAssist>();

        return GetSlaveMapperChannel(applicationConfigureAssist);
    }

    public static IMapperChannel GetSlaveMapperChannel(
        ApplicationConfigureAssist applicationConfigureAssist
    )
    {
        return new MapperChannel(
            "Slave",
            applicationConfigureAssist.GetSlaveDatabaseConnection().Value,
            RelationDatabaseTypeAssist.GetRelationDatabaseType(
                applicationConfigureAssist.GetSlaveDatabaseType().Value
            )
        );
    }

    public static IMapperChannel GetHistoryMapperChannel()
    {
        var applicationConfigureAssist = ServiceProviderAssist.GetProvider()
            .GetRequiredService<ApplicationConfigureAssist>();

        return GetHistoryMapperChannel(applicationConfigureAssist);
    }

    public static IMapperChannel GetHistoryMapperChannel(
        ApplicationConfigureAssist applicationConfigureAssist
    )
    {
        return new MapperChannel(
            "History",
            applicationConfigureAssist.GetHistoryDatabaseConnection().Value,
            RelationDatabaseTypeAssist.GetRelationDatabaseType(
                applicationConfigureAssist.GetHistoryDatabaseType().Value
            )
        );
    }
}