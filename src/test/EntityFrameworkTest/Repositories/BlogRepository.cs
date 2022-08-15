using EntityFrameworkTest.Entities;
using EntityFrameworkTest.IRepositories;
using EasySoft.Core.Mvc.Framework.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using EasySoft.UtilityTools.Enums;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.Repositories;

public class BlogRepository : Repository<Blog>, IBlogRepository
{
    public BlogRepository(DbContext context) : base(context)
    {
    }

    public async Task<ExecutiveResult<Blog>> GetBlog(int blogId)
    {
        var author = await DBSet.FindAsync(blogId);

        if (author == null)
        {
            return new ExecutiveResult<Blog>(ReturnCode.NoData)
            {
                Data = new Blog()
            };
        }

        return new ExistResult<Blog>(ReturnCode.Ok)
        {
            Data = author
        };
    }
}