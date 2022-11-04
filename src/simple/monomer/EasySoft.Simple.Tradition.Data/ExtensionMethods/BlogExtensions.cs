using EasySoft.Simple.Tradition.Data.DataTransferObjects;
using EasySoft.Simple.Tradition.Data.Entities;
using Mapster;

namespace EasySoft.Simple.Tradition.Data.ExtensionMethods;

public static class BlogExtensions
{
    public static BlogDto ToBlogDto(this Blog blog)
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<Blog, BlogDto>()
            .Map(
                dest => dest.BlogId,
                src => src.Id
            );

        var dto = blog.Adapt<BlogDto>(typeAdapterConfig);

        blog.Adapt(dto);

        return dto;
    }
}