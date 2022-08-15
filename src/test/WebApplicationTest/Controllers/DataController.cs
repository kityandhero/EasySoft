using AutoFacTest.Interfaces;
using EntityFrameworkTest.Contexts;
using EntityFrameworkTest.IServices;
using Framework.Controllers;
using Framework.DatabaseAssists;
using Framework.ExtensionMethods;
using Framework.IocAssists;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasySoft.UtilityTools.Enums;

namespace WebApplicationTest.Controllers;

public class DataController : CustomControllerBase
{
    private DbContext _context;

    private IAuthorService _authorService;

    public DataController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    // public DataController(DbContext context, IAuthorService authorService)
    // {
    //     context = context;
    //     _authorService = authorService;
    // }

    public async Task<IActionResult> Initialize()
    {
        DatabaseAssist.Initialize(_context);

        return this.Success(ReturnCode.Ok);
    }

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