using EasySoft.Simple.Tradition.Data.DataTransferObjects;
using EasySoft.Simple.Tradition.Data.Entities;
using Mapster;

namespace EasySoft.Simple.Tradition.Data.ExtensionMethods;

public static class AuthorExtensions
{
    public static AuthorDto ToAuthorDto(this Author author)
    {
        var authorDto = author.Adapt<AuthorDto>();

        author.Adapt(authorDto);

        return authorDto;
    }
}