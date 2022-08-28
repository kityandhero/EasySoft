namespace EasySoft.Core.Dapper.Interfaces
{
    public interface IMapper
    {
        IMapperChannel GetMapperChannel();

        IMapperTransaction GetDbTransaction();
    }
}