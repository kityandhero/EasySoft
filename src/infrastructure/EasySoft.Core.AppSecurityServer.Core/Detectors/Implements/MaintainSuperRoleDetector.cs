using EasySoft.Core.AppSecurityServer.Core.Clients;
using EasySoft.Core.AppSecurityServer.Core.Detectors.Interfaces;
using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Detectors.Implements;

/// <inheritdoc />
public class MaintainSuperRoleDetector : IMaintainSuperRoleDetector
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IWebHostEnvironment _environment;
    private readonly IMaintainSuperRoleClient _maintainSuperRoleClient;
    private readonly IAppSecurityService _appSecurityService;

    /// <summary>
    /// 访问探测器
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="environment"></param>
    /// <param name="maintainSuperRoleClient"></param>
    /// <param name="appSecurityService"></param>
    public MaintainSuperRoleDetector(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment,
        IMaintainSuperRoleClient maintainSuperRoleClient,
        IAppSecurityService appSecurityService
    )
    {
        _loggerFactory = loggerFactory;
        _environment = environment;
        _maintainSuperRoleClient = maintainSuperRoleClient;
        _appSecurityService = appSecurityService;
    }

    /// <inheritdoc />
    public async Task MaintainSuper()
    {
        var resultGetMainControlAppSecurity = await _appSecurityService.GetMainControlAppSecurity();

        if (!resultGetMainControlAppSecurity.Success || resultGetMainControlAppSecurity.Data == null) return;

        var mainControlAppSecurity = resultGetMainControlAppSecurity.Data;

        var appId = mainControlAppSecurity.AppId;
        var salt = AppSecurityAssist.GetSalt();
        var sign = AppSecurityAssist.SignRequest(
            appId,
            AppSecurityClientConfigure.GetPublicKey(),
            salt
        );

        var list = (await _appSecurityService.SingleListNeedMaintain()).ToList();

        if (_environment.IsDevelopment())
            if (list.Any())
                _loggerFactory.CreateLogger<object>().LogAdvancePrompt(
                    $"Maintain super role, total count {list.Count()}"
                );

        async void MaintainEvery(AppSecurityDto o)
        {
            await _maintainSuperRoleClient.MaintainSuper(o.Channel, appId, sign, salt);

            await _appSecurityService.SetSuperNextMaintainTime(o);
        }

        list.ForEach(MaintainEvery);
    }
}