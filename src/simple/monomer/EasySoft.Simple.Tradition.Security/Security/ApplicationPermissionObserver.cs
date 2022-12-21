using EasySoft.Simple.Tradition.Service.Services.Interfaces;

namespace EasySoft.Simple.Tradition.Security.Security;

/// <summary>
/// ApplicationPermissionObserver
/// </summary>
public class ApplicationPermissionObserver : PermissionObserverCore
{
    private readonly IUserService _userService;
    private readonly ICompetenceDetector _competenceDetector;

    /// <summary>
    /// ApplicationPermissionObserver
    /// </summary>
    /// <param name="applicationActualOperator"></param>
    /// <param name="competenceDetector"></param>
    /// <param name="userService"></param>
    public ApplicationPermissionObserver(
        IActualOperator applicationActualOperator,
        ICompetenceDetector competenceDetector,
        IUserService userService
    ) : base(applicationActualOperator)
    {
        _competenceDetector = competenceDetector;
        _userService = userService;
    }

    /// <summary>
    /// OnJudging
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override bool OnJudging()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// GetCompetenceEntityCollection
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override async Task<IList<CompetenceEntity>> GetCompetenceEntityCollectionAsync()
    {
        var identification = GetActualOperator().GetIdentification();

        if (identification == null || !identification.IsInt64(out var id)) return new List<CompetenceEntity>();

        var result = await _userService.GetRoleGroupIdAsync(id);

        if (!result.Success) return new List<CompetenceEntity>();

        var list = await _competenceDetector.GetCompetenceEntityCollection(result.Data);

        return list;
    }
}