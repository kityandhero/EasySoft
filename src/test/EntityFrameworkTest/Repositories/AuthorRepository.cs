using EntityFrameworkTest.Contexts;
using EntityFrameworkTest.Entities;
using EntityFrameworkTest.IRepositories;
using EasySoft.Core.Mvc.Framework.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using EasySoft.UtilityTools.Enums;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.Repositories;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(DataContext context) : base(context)
    {
    }

    public async Task<ExecutiveResult<Author>> GetAuthor(int authorId)
    {
        var author = await DBSet.FindAsync(authorId);

        if (author == null)
        {
            return new ExecutiveResult<Author>(ReturnCode.NoData)
            {
                Data = new Author()
            };
        }

        return new ExistResult<Author>(ReturnCode.Ok)
        {
            Data = author
        };
    }
}