using EasySoft.Core.Web.Framework.Services;
using EasySoft.Simple.EntityFrameworkCore.Entities;
using EasySoft.Simple.Shared.DataTransfer;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.EntityFrameworkCore.IServices;

public interface IAuthorService : IService
{
    public Task<ExecutiveResult<AuthorDto>> GetAuthorDtoSync(int authorId);

    public Task<ExecutiveResult<Author>> RegisterAsync(string loginName, string password);

    public ExecutiveResult<Author> SignIn(string loginName, string password);
}