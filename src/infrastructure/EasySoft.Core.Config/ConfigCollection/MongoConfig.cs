using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

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