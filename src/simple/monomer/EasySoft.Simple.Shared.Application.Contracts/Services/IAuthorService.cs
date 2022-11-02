using EasySoft.Core.Data.Attributes;
using EasySoft.Core.Infrastructure.Services;
using EasySoft.Simple.Domain.Aggregates.AuthorAggregate;
using EasySoft.Simple.Shared.Application.Contracts.DataTransfer;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.Shared.Application.Contracts.Services;

public interface IAuthorService : IBusinessService
{
    public Task<ExecutiveResult<AuthorDto>> GetAuthorDtoSync(int authorId);

    public Task<ExecutiveResult<Author>> RegisterAsync(string loginName, string password);

    [UnitOfWork]
    public Task<ExecutiveResult> RegisterMultiAsync(Dictionary<string, string> namePassword);

    public Task<ExecutiveResult<Author>> SignInAsync(string loginName, string password);

    [UnitOfWork(SharedToCap = true)]
    public Task<ExecutiveResult<Author>> UpdateFirstAuthor(string loginName, string password);
}