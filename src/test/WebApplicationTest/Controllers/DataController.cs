using EasySoft.Core.Web.Framework.Controllers;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EntityFrameworkTest.IServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Controllers;

public class DataController : CustomControllerBase
{
    private readonly IAuthorService _authorService;

    public DataController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    // public DataController(DbContext context, IAuthorService authorService)
    // {
    //     context = context;
    //     _authorService = authorService;
    // }

    public async Task<IActionResult> GetAuthor()
    {
        var result = await _authorService.GetAuthor(0);

        return !result.Success ? this.Fail(result.Code) : ControllerExtensions.Success(this, result.Data);
    }
}