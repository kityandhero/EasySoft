using EasySoft.Core.EntityFramework.Repositories;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;
using EntityFrameworkTest.Contexts;
using EntityFrameworkTest.Entities;
using EntityFrameworkTest.IRepositories;

namespace EntityFrameworkTest.Repositories;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(DataContext context) : base(context)
    {
    }

    public async Task<ExecutiveResult<Author>> GetAuthor(int authorId)
    {
        var author = await GetDbSet().FindAsync(authorId);

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