using EasySoft.Core.EntityFramework.Repositories;
using EasySoft.Simple.EntityFrameworkCore.Entities;
using EasySoft.Simple.EntityFrameworkCore.IRepositories;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.EntityFrameworkCore.Repositories;

public class BlogRepository : Repository<Blog>, IBlogRepository
{
    public BlogRepository(DbContext context) : base(context)
    {
    }

    public async Task<ExecutiveResult<Blog>> GetBlog(int blogId)
    {
        var author = await GetDbSet().FindAsync(blogId);

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