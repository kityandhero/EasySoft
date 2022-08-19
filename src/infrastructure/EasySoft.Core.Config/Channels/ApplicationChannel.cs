namespace EasySoft.Core.Config.Channels;

public class ApplicationChannel : IApplicationChannel
{
    private int _channel;

    public int GetChannel()
    {
        return _channel;
    }

    public IApplicationChannel SetChannel(int channel)
    {
        _channel = channel;

        return this;
    }
}