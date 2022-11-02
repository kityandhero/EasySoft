using EasySoft.Core.Data.Attributes;
using EasySoft.Core.Infrastructure.Services;
using EasySoft.Simple.WebLog.Domain.Aggregates.AuthorAggregate;
using EasySoft.Simple.WebLog.Application.Contracts.DataTransferObjects;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.WebLog.Application.Contracts.Services;

public interface IAuthorService : IBusinessService
{
    public Task<ExecutiveResult<AuthorDto>> GetAuthorDtoSync(int authorId);

    [UnitOfWork(SharedToCap = true)]
    public Task<ExecutiveResult<Author>> UpdateFirstAuthor();
}