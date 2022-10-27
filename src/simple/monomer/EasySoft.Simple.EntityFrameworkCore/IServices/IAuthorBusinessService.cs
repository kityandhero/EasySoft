using EasySoft.Core.Data.Attributes;
using EasySoft.Core.Infrastructure.Services;

namespace EasySoft.Simple.EntityFrameworkCore.IServices;

public interface IAuthorBusinessService : IBusinessService
{
    public Task<ExecutiveResult<AuthorDto>> GetAuthorDtoSync(int authorId);

    public Task<ExecutiveResult<Author>> RegisterAsync(string loginName, string password);

    [UnitOfWork]
    public Task<ExecutiveResult> RegisterMultiAsync(Dictionary<string, string> namePassword);

    public Task<ExecutiveResult<Author>> SignInAsync(string loginName, string password);
}