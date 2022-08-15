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

        if (!result.Success)
        {
            return this.Fail(result.Code);
        }

        return this.Success(result.Data);
    }
}