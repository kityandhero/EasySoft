using EntityFrameworkTest.Entities;
using EntityFrameworkTest.IRepositories;
using EntityFrameworkTest.IServices;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;

    public BlogService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public Task<ExecutiveResult<Blog>> GetBlog(int authorId)
    {
        return _blogRepository.GetBlog(authorId);
    }
}