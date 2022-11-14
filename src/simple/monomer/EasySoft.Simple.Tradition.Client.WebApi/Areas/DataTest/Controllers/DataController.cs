using EasySoft.Core.Data.Contexts.Interfaces;
using EasySoft.Core.Data.Transactions;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Simple.Tradition.Data.ExtensionMethods;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.Tradition.Client.WebApi.Areas.DataTest.Controllers;

/// <summary>
///     DataController
/// </summary>
public class DataController : AreaControllerCore
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserService _userService;

    private readonly IBlogService _blogService;

    private readonly DbContext _dataContext;

    /// <summary>
    ///     DataController
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="userService"></param>
    /// <param name="blogService"></param>
    /// <param name="dataContext"></param>
    public DataController(
        IUnitOfWork unitOfWork,
        IUserService userService,
        IBlogService blogService,
        DbContext dataContext
    )
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _blogService = blogService;
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
    public async Task<IActionResult> GetBlog()
    {
        var result = await _blogService.GetFirstAsync();

        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data?.ToBlogDto());
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

            await _userService.RegisterMultiAsync(dictionary);

            var result = await _blogService.GetBlogDtoSync(1);

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
    /// UpdateFirstBlog  
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> UpdateFirstBlog()
    {
        var result = await _blogService.UpdateFirst();

        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data);
    }
}