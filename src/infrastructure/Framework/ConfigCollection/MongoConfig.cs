using Framework.ConfigInterface;

namespace Framework.ConfigCollection;

public class MongoConfig : IConfig
{
    public static readonly MongoConfig Instance = new();

    public string Connection { get; set; }

    public string Database { get; set; }

    public MongoConfig()
    {
        Connection = "";
        Database = "";
    }
}