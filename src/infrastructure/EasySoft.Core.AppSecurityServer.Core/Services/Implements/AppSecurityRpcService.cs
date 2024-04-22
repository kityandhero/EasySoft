using EasySoft.Core.AppSecurityServer.Core.Entities;
using EasySoft.Core.AppSecurityServer.Core.Extensions;
using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Services.Implements;

/// <inheritdoc />
public class AppSecurityRpcService : IAppSecurityRpcService
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IWebHostEnvironment _environment;
    private readonly IRepository<AppSecurity> _appSecurityRepository;
    private readonly IRepository<AppPublicKey> _appPublicKeyRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="environment"></param>
    /// <param name="appSecurityRepository"></param>
    /// <param name="appPublicKeyRepository"></param>
    public AppSecurityRpcService(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment,
        IRepository<AppSecurity> appSecurityRepository,
        IRepository<AppPublicKey> appPublicKeyRepository
    )
    {
        _appSecurityRepository = appSecurityRepository;
        _appPublicKeyRepository = appPublicKeyRepository;
        _loggerFactory = loggerFactory;
        _environment = environment;
    }

    /// <inheritdoc />
    public async Task<ExecutiveResult> ChannelCheckAsync(string channel)
    {
        var result = await _appSecurityRepository.GetAsync(
            o => o.Channel == channel
        );

        if (!result.Success)
        {
            if (_environment.IsDevelopment())
            {
                _loggerFactory.CreateLogger<object>().LogAdvanceError(result.Message);
            }

            return new ExecutiveResult(
                result.Code
            );
        }

        if (result.Data == null)
        {
            var message = $"channel -> {channel} check error.";

            if (_environment.IsDevelopment())
            {
                _loggerFactory.CreateLogger<object>().LogAdvanceError(message);
            }

            return new ExecutiveResult(
                result.Code
            );
        }

        return ExecutiveResultAssist.CreateOk();
    }

    /// <inheritdoc />
    public async Task<ExecutiveResult<AppPublicKeyDto>> CredentialVerifyAsync(AppSecurityDto appSecurityDto)
    {
        if (appSecurityDto.UnixTime < 0)
        {
            return new ExecutiveResult<AppPublicKeyDto>(
                ReturnCode.ParamError.ToMessage(
                    $"unixTime params error -> {appSecurityDto.BuildInfo()}"
                )
            );
        }

        if (string.IsNullOrWhiteSpace(appSecurityDto.Salt))
        {
            return new ExecutiveResult<AppPublicKeyDto>(
                ReturnCode.ParamError.ToMessage(
                    $"salt params error -> {appSecurityDto.BuildInfo()}"
                )
            );
        }

        if (string.IsNullOrWhiteSpace(appSecurityDto.AppId))
        {
            return new ExecutiveResult<AppPublicKeyDto>(
                ReturnCode.ParamError.ToMessage(
                    $"appId params error -> {appSecurityDto.BuildInfo()}"
                )
            );
        }

        if (string.IsNullOrWhiteSpace(appSecurityDto.Channel))
        {
            return new ExecutiveResult<AppPublicKeyDto>(
                ReturnCode.ParamError.ToMessage(
                    $"channel params error -> {appSecurityDto.BuildInfo()}"
                )
            );
        }

        if (string.IsNullOrWhiteSpace(appSecurityDto.Sign))
        {
            return new ExecutiveResult<AppPublicKeyDto>(
                ReturnCode.ParamError.ToMessage(
                    $"sign params error -> {appSecurityDto.BuildInfo()}"
                )
            );
        }

        IAppSecurity appSecurity;

        //仅用于内部集成模式，此模式下忽略校验
        if (appSecurityDto.AppId == AppSecurityAssist.EmbedAppId)
        {
            appSecurity = new AppSecurityDto
            {
                AppId = appSecurityDto.AppId,
                AppSecret = appSecurityDto.AppSecret
            };
        }
        else
        {
            var result = await _appSecurityRepository.GetAsync(
                o => o.AppId == appSecurityDto.AppId
                     && o.AppSecret == appSecurityDto.AppSecret
                     && o.Channel == appSecurityDto.Channel
            );

            if (!result.Success)
            {
                if (_environment.IsDevelopment())
                {
                    _loggerFactory.CreateLogger<object>().LogAdvanceError(result.Message);
                }

                return new ExecutiveResult<AppPublicKeyDto>(
                    result.Code
                );
            }

            if (result.Data == null)
            {
                var message = $"appid {appSecurityDto.AppId} do not exist.";

                if (_environment.IsDevelopment())
                {
                    _loggerFactory.CreateLogger<object>().LogAdvanceError(message);
                }

                return new ExecutiveResult<AppPublicKeyDto>(
                    result.Code
                );
            }

            appSecurity = result.Data;
        }

        var sign = AppSecurityAssist.SignVerify(
            appSecurity,
            appSecurityDto.UnixTime,
            appSecurityDto.Salt
        );

        if (appSecurityDto.Sign != sign)
        {
            return new ExecutiveResult<AppPublicKeyDto>(ReturnCode.VerifyError.ToMessage("sign error"));
        }

        #region get public key after sign verify success

        var pageListResult = await _appPublicKeyRepository.PageListAsync(
            1,
            1
        );

        if (!pageListResult.Success)
        {
            return new ExecutiveResult<AppPublicKeyDto>(pageListResult.Code);
        }

        if (pageListResult.Count() > 0)
        {
            var list = pageListResult.List;

            return new ExecutiveResult<AppPublicKeyDto>(ReturnCode.Ok)
            {
                Data = list.First().ToAppPublicKeyDto()
            };
        }

        var appPublicKey = new AppPublicKey
        {
            Key = UniqueIdAssist.CreateUUID()
        };

        var resultGetPublicKey = await _appPublicKeyRepository.AddAsync(
            appPublicKey
        );

        if (resultGetPublicKey.Success)
        {
            return resultGetPublicKey.ToExecutiveResult(resultGetPublicKey.Data?.ToAppPublicKeyDto());
        }

        if (_environment.IsDevelopment())
        {
            _loggerFactory.CreateLogger<object>().LogAdvanceError(resultGetPublicKey.Message);
        }

        return resultGetPublicKey.ToExecutiveResult(resultGetPublicKey.Data?.ToAppPublicKeyDto());

        #endregion
    }
}