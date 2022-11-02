using EasySoft.Simple.WebLog.Domain.Aggregates.AuthorAggregate;
using EasySoft.Simple.WebLog.Application.Contracts.DataTransferObjects;
using Mapster;

namespace EasySoft.Simple.WebLog.Application.Contracts.ExtensionMethods;

public static class AuthorExtensions
{
    public static AuthorDto ToAuthorDto(this Author author)
    {
        var authorDto = author.Adapt<AuthorDto>();

        author.Adapt(authorDto);

        return authorDto;
    }
}