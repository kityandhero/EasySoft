using EntityFrameworkTest.Entities;
using EntityFrameworkTest.IRepositories;
using EntityFrameworkTest.IServices;
using EasySoft.UtilityTools.Result;

namespace EntityFrameworkTest.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public Task<ExecutiveResult<Author>> GetAuthor(int authorId)
    {
        return _authorRepository.GetAuthor(authorId);
    }
}