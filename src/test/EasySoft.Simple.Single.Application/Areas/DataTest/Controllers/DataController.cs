using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Simple.EntityFrameworkCore.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.Single.Application.Areas.DataTest.Controllers;

/// <summary>
/// DataController
/// </summary>
public class DataController : AreaControllerCore
{
    private readonly IAuthorService _authorService;

    /// <summary>
    /// DataController
    /// </summary>
    /// <param name="authorService"></param>
    public DataController(IAuthorService authorService)
    {
        _authorService = authorService;
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
        var result = await _authorService.GetAuthorDtoSync(0);

        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data);
    }
}