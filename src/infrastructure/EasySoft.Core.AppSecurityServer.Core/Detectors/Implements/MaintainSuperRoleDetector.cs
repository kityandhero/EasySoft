using EasySoft.Core.AppSecurityServer.Core.Clients;
using EasySoft.Core.AppSecurityServer.Core.Detectors.Interfaces;
using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Detectors.Implements;

/// <inheritdoc />
public class MaintainSuperRoleDetector : IMaintainSuperRoleDetector
{
    private readonly IMaintainSuperRoleClient _maintainSuperRoleClient;

    private readonly IAppSecurityService _appSecurityService;

    /// <summary>
    /// 访问探测器
    /// </summary>
    /// <param name="maintainSuperRoleClient"></param>
    /// <param name="appSecurityService"></param>
    public MaintainSuperRoleDetector(
        IMaintainSuperRoleClient maintainSuperRoleClient,
        IAppSecurityService appSecurityService
    )
    {
        _maintainSuperRoleClient = maintainSuperRoleClient;
        _appSecurityService = appSecurityService;
    }

    /// <inheritdoc />
    public async Task MaintainSuperRole()
    {
        var resultGetMainControlAppSecurity = await _appSecurityService.GerMainControlAppSecurity();

        if (!resultGetMainControlAppSecurity.Success || resultGetMainControlAppSecurity.Data == null) return;

        var mainControlAppSecurity = resultGetMainControlAppSecurity.Data;

        var appId = mainControlAppSecurity.AppId;
        var salt = AppSecurityAssist.GetSalt();
        var sign = AppSecurityAssist.SignRequest(
            appId,
            AppSecurityClientConfigure.GetPublicKey(),
            salt
        );

        var list = await _appSecurityService.SingleListNeedMaintain();

        list.ForEach(async o =>
        {
            await _maintainSuperRoleClient.MaintainSuperRole(
                o.Channel,
                appId,
                sign,
                salt
            );

            await _appSecurityService.SetSuperRoleNextMaintainTime(o);
        });
    }
}