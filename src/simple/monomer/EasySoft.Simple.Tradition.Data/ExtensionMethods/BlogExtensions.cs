using EasySoft.Simple.Tradition.Data.DataTransferObjects;
using EasySoft.Simple.Tradition.Data.Entities;
using Mapster;

namespace EasySoft.Simple.Tradition.Data.ExtensionMethods;

public static class BlogExtensions
{
    public static BlogDto ToBlogDto(this Blog blog)
    {
        var dto = blog.Adapt<BlogDto>();

        blog.Adapt(dto);

        return dto;
    }
}