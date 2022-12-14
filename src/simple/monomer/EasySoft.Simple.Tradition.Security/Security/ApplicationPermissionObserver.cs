using EasySoft.Core.PermissionVerification.Clients;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;
using EasySoft.UtilityTools.Standard.Exceptions;

namespace EasySoft.Simple.Tradition.Security.Security;

/// <summary>
/// ApplicationPermissionObserver
/// </summary>
public class ApplicationPermissionObserver : PermissionObserverCore
{
    private readonly IUserService _userService;
    private readonly IPermissionClient _permissionClient;

    /// <summary>
    /// ApplicationPermissionObserver
    /// </summary>
    /// <param name="applicationActualOperator"></param>
    /// <param name="permissionClient"></param>
    /// <param name="userService"></param>
    public ApplicationPermissionObserver(
        IActualOperator applicationActualOperator,
        IPermissionClient permissionClient,
        IUserService userService
    ) : base(applicationActualOperator)
    {
        _permissionClient = permissionClient;
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
    public override async Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync()
    {
        var identification = GetActualOperator().GetIdentification();

        if (identification == null || !identification.IsInt64(out var id)) return new List<CompetenceEntity>();

        var result = await _userService.GetRoleGroupIdAsync(id);

        if (!result.Success) return new List<CompetenceEntity>();

        var apiResponse = await _permissionClient.GetCompetenceEntityCollectionAsync(result.Data);

        if (!apiResponse.IsSuccessStatusCode)
            throw new UnknownException($"rpc {GetType().Name}.{nameof(GetCompetenceEntityCollectionAsync)} call fail");

        return apiResponse.Content ?? new List<CompetenceEntity>();
    }
}