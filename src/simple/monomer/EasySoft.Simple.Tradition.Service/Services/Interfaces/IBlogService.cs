using EasySoft.Core.Infrastructure.Services;
using EasySoft.Simple.Tradition.Data.DataTransferObjects;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.Tradition.Service.Services.Interfaces;

public interface IBlogService : IBusinessService
{
    Task<ExecutiveResult<Blog>> GetBlogAsync(int authorId);

    public Task<ExecutiveResult<Blog>> GetFirstAsync();

    public Task<ExecutiveResult<BlogDto>> GetBlogDtoSync(int authorId);

    public Task<ExecutiveResult<Blog>> UpdateFirst();
}