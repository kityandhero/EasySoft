namespace EasySoft.Core.Infrastructure.Channels;

public class ApplicationChannel : IApplicationChannel
{
    private int _channel;

    private string _name;

    public ApplicationChannel()
    {
        _name = "";
    }

    public int GetChannel()
    {
        return _channel;
    }

    public string GetName()
    {
        return _name;
    }

    public IApplicationChannel SetChannel(int channel)
    {
        _channel = channel;

        return this;
    }

    public IApplicationChannel SetName(string name)
    {
        _name = name;

        return this;
    }
}