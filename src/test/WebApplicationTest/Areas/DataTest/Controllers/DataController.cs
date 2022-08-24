﻿using EasySoft.Core.Web.Framework.ExtensionMethods;
using EntityFrameworkTest.IServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.DataTest.Controllers;

public class DataController : AreaControllerCore
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