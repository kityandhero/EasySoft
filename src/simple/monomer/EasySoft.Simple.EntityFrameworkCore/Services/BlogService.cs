using EasySoft.Core.Data.Repositories;
using EasySoft.Simple.EntityFrameworkCore.IServices;

namespace EasySoft.Simple.EntityFrameworkCore.Services;

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