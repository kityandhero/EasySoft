using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EntityFrameworkTest.Entities;
using EntityFrameworkTest.IServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.AuthTest.Controllers;

[Operator]
public class AuthorController : AreaControllerCore
{
    private readonly IActualOperator _actualOperator;
    private readonly IAuthorService _authorService;

    public AuthorController(IActualOperator actualOperator, IAuthorService authorService)
    {
        _actualOperator = actualOperator;
        _authorService = authorService;
    }

    public async Task<IActionResult> GetAuthor()
    {
        var result = await _authorService.GetAuthor(
            Convert.ToInt32(_actualOperator.GetIdentification())
        );


        return this.WrapperExecutiveResult(result);
    }
}