﻿using EasySoft.Simple.WebLog.Application.Contracts.DataTransferObjects;
using EasySoft.Simple.WebLog.Domain.Aggregates.BlogAggregate;
using Mapster;

namespace EasySoft.Simple.WebLog.Application.Contracts.ExtensionMethods;

public static class BlogExtensions
{
    public static BlogDto ToBlogDto(this Blog blog)
    {
        var typeAdapterConfig = new TypeAdapterConfig();

        typeAdapterConfig.ForType<Blog, BlogDto>().Map(dest => dest.BlogId, src => src.Id);

        var dto = blog.Adapt<BlogDto>(typeAdapterConfig);

        blog.Adapt(dto);

        return dto;
    }
}