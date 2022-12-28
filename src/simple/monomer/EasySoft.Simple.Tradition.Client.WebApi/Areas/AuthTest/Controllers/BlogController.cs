namespace EasySoft.Simple.Tradition.Client.WebApi.Areas.AuthTest.Controllers;

/// <summary>
/// AuthorController
/// </summary>
[Operator]
public class AuthorController : AreaControllerCore
{
    private readonly IBlogService _blogService;

    /// <summary>
    /// AuthorController
    /// </summary>
    /// <param name="blogService"></param>
    public AuthorController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    /// <summary>
    /// GetAuthor
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> GetAuthor()
    {
        var resultActualOperator = await this.GetActualOperator();

        if (!resultActualOperator.Success || resultActualOperator.Data == null)
            return this.Fail(resultActualOperator.Code);

        var result = await _blogService.GetBlogDtoAsync(
            Convert.ToInt32(resultActualOperator.Data.GetIdentity())
        );

        return this.WrapperExecutiveResult(result);
    }
}