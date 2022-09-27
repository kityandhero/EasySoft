using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Simple.EntityFrameworkCore.Contexts;
using EasySoft.Simple.EntityFrameworkCore.Entities;
using EasySoft.Simple.EntityFrameworkCore.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.Single.Application.Areas.DataTest.Controllers;

/// <summary>
/// DataController
/// </summary>
public class DataController : AreaControllerCore
{
    private readonly IAuthorService _authorService;

    private readonly DataContext _dataContext;

    /// <summary>
    /// DataController
    /// </summary>
    /// <param name="authorService"></param>
    public DataController(IAuthorService authorService, DataContext dataContext)
    {
        _authorService = authorService;

        _dataContext = dataContext;
    }

    // public DataController(DbContext context, IAuthorService authorService)
    // {
    //     context = context;
    //     _authorService = authorService;
    // }

    /// <summary>
    /// GetAuthor
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> GetAuthor()
    {
        await _dataContext.Database.EnsureDeletedAsync();
        await _dataContext.Database.EnsureCreatedAsync();

        var author = new Author
        {
            LoginName = "lili",
            Password = "123456"
        };

        _dataContext.Authors.Add(author);

        _dataContext.Posts.Add(new Post
        {
            Title = "this is .a simple post",
            Author = author
        });

        await _dataContext.SaveChangesAsync();

        var result = await _authorService.GetAuthorDtoSync(1);

        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data);
    }
}