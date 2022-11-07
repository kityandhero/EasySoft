namespace EasySoft.Core.Infrastructure.Assists;

public static class BusinessServiceAssist
{
    private static readonly List<Type> BusinessServiceInterfaceCollection;

    static BusinessServiceAssist()
    {
        BusinessServiceInterfaceCollection = new List<Type>();
    }

    public static void Add(Type type)
    {
        BusinessServiceInterfaceCollection.Add(type);
    }

    public static void AddRange(IEnumerable<Type> types)
    {
        BusinessServiceInterfaceCollection.AddRange(types);
    }

    public static ICollection<Type> GetBusinessServiceInterfaceCollection()
    {
        return BusinessServiceInterfaceCollection;
    }
}