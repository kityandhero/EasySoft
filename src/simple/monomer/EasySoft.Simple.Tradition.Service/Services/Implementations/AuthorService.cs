using EasySoft.Core.Data.Repositories;
using EasySoft.Simple.Tradition.Data.DataTransferObjects;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Data.ExtensionMethods;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.Tradition.Service.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IRepository<Author> _authorRepository;

    public AuthorService(IRepository<Author> repository)
    {
        _authorRepository = repository;
    }

    public async Task<ExecutiveResult<AuthorDto>> GetAuthorDtoSync(int authorId)
    {
        var result = await _authorRepository.GetAsync(authorId);

        if (!result.Success) return new ExecutiveResult<AuthorDto>(result.Code);

        if (result.Data != null)
            return new ExecutiveResult<AuthorDto>(result.Code)
            {
                Data = result.Data.ToAuthorDto()
            };

        return new ExecutiveResult<AuthorDto>(ReturnCode.NoData);
    }

    public async Task<ExecutiveResult<Author>> UpdateFirstAuthor()
    {
        var enumerable = await _authorRepository.SingleListAsync();

        var list = enumerable.ToList();

        if (!list.Any()) return new ExecutiveResult<Author>(ReturnCode.NoData);

        var first = list.First();

        // var result = await _authorRepository.GetAsync(first.Id);

        first.Motto = UniqueIdAssist.CreateUUID();

        var resultUpdate = await _authorRepository.UpdateAsync(first);

        if (resultUpdate.Success)
            return new ExecutiveResult<Author>(ReturnCode.Ok)
            {
                Data = resultUpdate.Data
            };

        return resultUpdate;
    }
}