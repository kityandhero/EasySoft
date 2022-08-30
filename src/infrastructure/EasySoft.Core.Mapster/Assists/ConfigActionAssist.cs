using Mapster;

namespace EasySoft.Core.Mapster.Assists;

public static class ConfigActionAssist
{
    private static readonly List<Action<TypeAdapterConfig>> ConfigActions = new();

    public static void AddConfigAction(Action<TypeAdapterConfig> action)
    {
        ConfigActions.Add(action);
    }

    public static List<Action<TypeAdapterConfig>> GetConfigAction()
    {
        return ConfigActions;
    }
}