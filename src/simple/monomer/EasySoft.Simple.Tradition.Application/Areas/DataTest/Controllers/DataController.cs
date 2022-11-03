using EasySoft.Core.Data.Transactions;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Simple.Tradition.Data.Contexts;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.Tradition.Application.Areas.DataTest.Controllers;

/// <summary>
///     DataController
/// </summary>
public class DataController : AreaControllerCore
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ICustomerService _customerService;

    private readonly IAuthorService _authorService;

    private readonly DataContext _dataContext;

    /// <summary>
    ///     DataController
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="customerService"></param>
    /// <param name="authorService"></param>
    /// <param name="dataContext"></param>
    public DataController(
        IUnitOfWork unitOfWork,
        ICustomerService customerService,
        IAuthorService authorService,
        DataContext dataContext
    )
    {
        _unitOfWork = unitOfWork;
        _customerService = customerService;
        _authorService = authorService;
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
            Motto = $"lili-{Guid.NewGuid().ToString()}",
            Pseudonym = "123456"
        };

        _dataContext.Authors.Add(author);

        _dataContext.Posts.Add(new Post
        {
            Title = "this is a simple post",
            Author = author
        });

        await _dataContext.SaveChangesAsync();

        var result = await _authorService.GetAuthorDtoSync(1);

        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data);
    }

    /// <summary>
    ///     使用工作单元操控
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> GetAuthorWithUnitOfWork()
    {
        try
        {
            _unitOfWork.BeginTransaction(distributed: false);

            var dictionary = new Dictionary<string, string>();

            for (var i = 0; i < 10; i++) dictionary.Add($"lili-{Guid.NewGuid().ToString()}", "123456");

            await _customerService.RegisterMultiAsync(dictionary);

            var result = await _authorService.GetAuthorDtoSync(1);

            await _unitOfWork.CommitAsync();

            return !result.Success ? this.Fail(result.Code) : this.Success(result.Data);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return this.Fail(ReturnCode.Exception.ToMessage(ex.Message));
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    /// <summary>
    /// UpdateFirstAuthor  
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> UpdateFirstAuthor()
    {
        var result = await _authorService.UpdateFirstAuthor();

        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data);
    }
}