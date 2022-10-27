using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.Simple.EntityFrameworkCore.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.Single.Application.Areas.AuthTest.Controllers;

/// <summary>
/// AuthorController
/// </summary>
[Operator]
public class AuthorController : AreaControllerCore
{
    private readonly IAuthorBusinessService _authorBusinessService;

    /// <summary>
    /// AuthorController
    /// </summary>
    /// <param name="authorBusinessService"></param>
    public AuthorController(IAuthorBusinessService authorBusinessService)
    {
        _authorBusinessService = authorBusinessService;
    }

    /// <summary>
    /// GetAuthor
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> GetAuthor()
    {
        var resultActualOperator = await this.GetActualOperator();

        if (!resultActualOperator.Success || resultActualOperator.Data == null)
        {
            return this.Fail(resultActualOperator.Code);
        }

        var result = await _authorBusinessService.GetAuthorDtoSync(
            Convert.ToInt32(resultActualOperator.Data.GetIdentification())
        );

        return this.WrapperExecutiveResult(result);
    }
}