using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Simple.EntityFrameworkCore.Contexts;
using EasySoft.Simple.EntityFrameworkCore.Entities;
using EasySoft.Simple.EntityFrameworkCore.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.Single.Application.Areas.DataTest.Controllers;

/// <summary>
///     DataController
/// </summary>
public class DataController : AreaControllerCore
{
    private readonly IAuthorBusinessService _authorBusinessService;

    private readonly DataContext _dataContext;

    /// <summary>
    ///     DataController
    /// </summary>
    /// <param name="authorBusinessService"></param>
    /// <param name="dataContext"></param>
    public DataController(IAuthorBusinessService authorBusinessService, DataContext dataContext)
    {
        _authorBusinessService = authorBusinessService;

        _dataContext = dataContext;
    }

    // public DataController(DbContext context, IAuthorService authorService)
    // {
    //     context = context;
    //     _authorService = authorService;
    // }

    /// <summary>
    ///     初始化数据库
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> InitDatabase()
    {
        await _dataContext.Database.EnsureDeletedAsync();
        await _dataContext.Database.EnsureCreatedAsync();

        return this.Success();
    }

    /// <summary>
    ///     GetAuthor
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> GetAuthor()
    {
        var author = new Author
        {
            LoginName = $"lili-{Guid.NewGuid().ToString()}",
            Password = "123456"
        };

        _dataContext.Authors.Add(author);

        _dataContext.Posts.Add(new Post
        {
            Title = "this is a simple post",
            Author = author
        });

        await _dataContext.SaveChangesAsync();

        var result = await _authorBusinessService.GetAuthorDtoSync(1);

        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data);
    }

    /// <summary>
    ///     使用工作单元操控
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> GetAuthorWithUnitOfWork()
    {
        var dictionary = new Dictionary<string, string>();

        for (var i = 0; i < 10; i++) dictionary.Add($"lili-{Guid.NewGuid().ToString()}", "123456");

        await _authorBusinessService.RegisterMultiAsync(dictionary);

        var result = await _authorBusinessService.GetAuthorDtoSync(1);

        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data);
    }
}