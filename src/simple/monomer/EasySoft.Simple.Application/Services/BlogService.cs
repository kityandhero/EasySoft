using EasySoft.Core.Data.Repositories;
using EasySoft.Simple.Application.Contracts.Services;
using EasySoft.Simple.Domain.Aggregates.AuthorAggregate;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.Application.Services;

public class BlogService : IBlogService
{
    private readonly IRepository<Blog> _blogRepository;

    public BlogService(IRepository<Blog> blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<ExecutiveResult<Blog>> GetBlogAsync(int blogId)
    {
        return await _blogRepository.GetAsync(blogId);
    }
}