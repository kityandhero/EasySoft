using EasySoft.Core.Data.Repositories;
using EasySoft.Simple.EntityFrameworkCore.IServices;

namespace EasySoft.Simple.EntityFrameworkCore.Services;

public class BlogBusinessService : IBlogBusinessService
{
    private readonly IRepository<Blog> _blogRepository;

    public BlogBusinessService(IRepository<Blog> blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<ExecutiveResult<Blog>> GetBlogAsync(int blogId)
    {
        return await _blogRepository.GetAsync(blogId);
    }
}