using EasySoft.Simple.EntityFrameworkCore.Entities;
using EasySoft.Simple.Shared.DataTransfer;
using Mapster;

namespace EasySoft.Simple.EntityFrameworkCore.ExtensionMethods;

public static class AuthorExtensions
{
    public static AuthorDto ToAuthorDto(this Author author)
    {
        var authorDto = author.Adapt<AuthorDto>();

        author.Adapt(authorDto);

        return authorDto;
    }
}