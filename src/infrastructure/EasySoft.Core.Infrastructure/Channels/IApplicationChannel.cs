namespace EasySoft.Core.Infrastructure.Channels;

public interface IApplicationChannel
{
    public int GetChannel();

    public string GetName();

    public IApplicationChannel SetChannel(int channel);

    public IApplicationChannel SetName(string name);
}