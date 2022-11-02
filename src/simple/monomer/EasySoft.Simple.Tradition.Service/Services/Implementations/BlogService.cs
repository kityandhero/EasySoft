using EasySoft.Core.Data.Repositories;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.Tradition.Service.Services.Implementations;

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