using MapsterMapper;

namespace EasySoft.Core.Mapster.Assists;

public static class MapperAssist
{
    private static IMapper? _mapper;

    public static void SetMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public static IMapper GetMapper()
    {
        if (_mapper == null)
        {
            throw new Exception("please set mapper before get it");
        }

        return _mapper;
    }
}