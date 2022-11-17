namespace EasySoft.Core.Infrastructure.Assists;

/// <summary>
/// BusinessServiceAssist
/// </summary>
public static class BusinessServiceAssist
{
    private static readonly List<Type> BusinessServiceInterfaceCollection;

    static BusinessServiceAssist()
    {
        BusinessServiceInterfaceCollection = new List<Type>();
    }

    /// <summary>
    /// Add
    /// </summary>
    /// <param name="type"></param>
    public static void Add(Type type)
    {
        BusinessServiceInterfaceCollection.Add(type);
    }

    /// <summary>
    /// AddRange
    /// </summary>
    /// <param name="types"></param>
    public static void AddRange(IEnumerable<Type> types)
    {
        BusinessServiceInterfaceCollection.AddRange(types);
    }

    /// <summary>
    /// GetBusinessServiceInterfaceCollection
    /// </summary>
    /// <returns></returns>
    public static ICollection<Type> GetBusinessServiceInterfaceCollection()
    {
        return BusinessServiceInterfaceCollection;
    }
}