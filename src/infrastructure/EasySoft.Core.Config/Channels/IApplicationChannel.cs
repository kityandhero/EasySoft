namespace EasySoft.Core.Config.Channels;

public interface IApplicationChannel
{
    public int GetChannel();

    public IApplicationChannel SetChannel(int channel);
}