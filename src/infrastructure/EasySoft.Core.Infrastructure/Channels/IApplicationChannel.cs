namespace EasySoft.Core.Infrastructure.Channels;

public interface IApplicationChannel
{
    public int GetChannel();

    public IApplicationChannel SetChannel(int channel);
}