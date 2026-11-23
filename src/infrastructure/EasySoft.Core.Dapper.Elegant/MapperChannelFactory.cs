using EasySoft.Core.Infrastructure.Assists;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.Dapper.Elegant;

public static class MapperChannelFactory
{
    public static IMapperChannel GetMasterMapperChannel()
    {
        var infrastructureConfigureAssist = ServiceProviderAssist.GetProvider()
            .GetRequiredService<InfrastructureConfigureAssist>();

        return GetMasterMapperChannel(infrastructureConfigureAssist);
    }

    public static IMapperChannel GetMasterMapperChannel(
        InfrastructureConfigureAssist infrastructureConfigureAssist
    )
    {
        return new MapperChannel(
            "Master",
            infrastructureConfigureAssist.GetMasterDatabaseConnection().Value,
            RelationDatabaseTypeAssist.GetRelationDatabaseType(
                infrastructureConfigureAssist.GetHistoryDatabaseType().Value
            )
        );
    }

    public static IMapperChannel GetSlaveMapperChannel()
    {
        var infrastructureConfigureAssist = ServiceProviderAssist.GetProvider()
            .GetRequiredService<InfrastructureConfigureAssist>();

        return GetSlaveMapperChannel(infrastructureConfigureAssist);
    }

    public static IMapperChannel GetSlaveMapperChannel(
        InfrastructureConfigureAssist infrastructureConfigureAssist
    )
    {
        return new MapperChannel(
            "Slave",
            infrastructureConfigureAssist.GetSlaveDatabaseConnection().Value,
            RelationDatabaseTypeAssist.GetRelationDatabaseType(
                infrastructureConfigureAssist.GetSlaveDatabaseType().Value
            )
        );
    }

    public static IMapperChannel GetHistoryMapperChannel()
    {
        var infrastructureConfigureAssist = ServiceProviderAssist.GetProvider()
            .GetRequiredService<InfrastructureConfigureAssist>();

        return GetHistoryMapperChannel(infrastructureConfigureAssist);
    }

    public static IMapperChannel GetHistoryMapperChannel(
        InfrastructureConfigureAssist infrastructureConfigureAssist
    )
    {
        return new MapperChannel(
            "History",
            infrastructureConfigureAssist.GetHistoryDatabaseConnection().Value,
            RelationDatabaseTypeAssist.GetRelationDatabaseType(
                infrastructureConfigureAssist.GetHistoryDatabaseType().Value
            )
        );
    }
}