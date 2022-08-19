namespace WebApplicationTest.Enums;

public enum ApplicationChannelCollection
{
    TestApplication = 100
}

public static class ApplicationChannelCollectionExtensions
{
    public static int ToInt(this ApplicationChannelCollection source)
    {
        return (int)source;
    }
}