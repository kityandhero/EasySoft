using EntityFrameworkTest.IServices;
using EasySoft.Core.Mvc.Framework.Controllers;
using EasySoft.Core.Mvc.Framework.ExtensionMethods;
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

        return !result.Success ? this.Fail(result.Code) : this.Success(result.Data);
    }
}