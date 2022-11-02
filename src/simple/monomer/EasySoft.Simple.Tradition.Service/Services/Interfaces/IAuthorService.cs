using EasySoft.Core.Data.Attributes;
using EasySoft.Core.Infrastructure.Services;
using EasySoft.Simple.Tradition.Data.DataTransferObjects;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.Tradition.Service.Services.Interfaces;

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