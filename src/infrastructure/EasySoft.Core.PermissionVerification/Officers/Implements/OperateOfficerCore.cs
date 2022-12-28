using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.PermissionVerification.Observers;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.PermissionVerification.Officers.Implements;

/// <summary>
/// OperateOfficerCore
/// </summary>
public abstract class OperateOfficerCore : AccessWayOfficer
{
    private readonly IActualOperator _actualOperator;

    /// <summary>
    /// OperateOfficerCore
    /// </summary>
    protected OperateOfficerCore(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment,
        IMediator mediator
    ) : base(loggerFactory, environment, mediator)
    {
        _actualOperator = AutofacAssist.Instance.Resolve<IActualOperator>();
    }

    private IPermissionObserver? _permissionObserver;

    private IActualOperator GetOperator()
    {
        if (!Environment.IsDevelopment()) return _actualOperator;

        var logger = LoggerFactory.CreateLogger<OperateOfficerCore>();

        logger.LogAdvanceExecute(
            $"{nameof(OperateOfficerCore)}.{nameof(GetOperator)}"
        );

        return _actualOperator;
    }

    /// <summary>
    /// GetPermissionObserver
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected IPermissionObserver GetPermissionObserver()
    {
        if (Environment.IsDevelopment())
        {
            var logger = LoggerFactory.CreateLogger<OperateOfficerCore>();

            logger.LogAdvanceExecute(
                $"{nameof(OperateOfficerCore)}.{nameof(GetPermissionObserver)}"
            );
        }

        if (!AutofacAssist.Instance.IsRegistered<IPermissionObserver>())
            throw new Exception("IPermissionObserver is not injected");

        if (_permissionObserver == null) throw new Exception("PermissionObserver has not set");

        return _permissionObserver;
    }

    private void PrePareVerify()
    {
        if (Environment.IsDevelopment())
        {
            var logger = LoggerFactory.CreateLogger<OperateOfficerCore>();

            logger.LogAdvanceExecute(
                $"{nameof(OperateOfficerCore)}.{nameof(PrePareVerify)}"
            );
        }

        _permissionObserver = AutofacAssist.Instance.Resolve<IPermissionObserver>();
    }

    /// <summary>
    /// 检测访问权限
    /// </summary>
    /// <returns></returns>
    private async Task<ExecutiveResult> CheckAccessPermissionAsync()
    {
        if (Environment.IsDevelopment())
        {
            var logger = LoggerFactory.CreateLogger<OperateOfficerCore>();

            logger.LogAdvanceExecute(
                $"{nameof(OperateOfficerCore)}.{nameof(CheckAccessPermissionAsync)}"
            );
        }

        var applicationOperator = GetOperator();

        if (applicationOperator.IsAnonymous())
            return new ExecutiveResult(
                ReturnCode.NoPermission.ToMessage(
                    $"匿名用户不支持鉴权, 请修复程序（配置登录验证 {nameof(OperatorAttribute)}）"
                )
            );

        if (string.IsNullOrWhiteSpace(AccessPermission.GuidTag)) return new ExecutiveResult(ReturnCode.Ok);

        if (_permissionObserver == null) throw new Exception("PermissionObserver has not set");

        return await _permissionObserver.CheckAccessPermissionAsync(AccessPermission.GuidTag);
    }

    /// <summary>
    /// TryVerification
    /// </summary>
    /// <returns></returns>
    protected async Task<ExecutiveResult<ApiResult>> TryVerifyAsync()
    {
        if (Environment.IsDevelopment())
        {
            var logger = LoggerFactory.CreateLogger<OperateOfficerCore>();

            logger.LogAdvanceExecute(
                $"{nameof(OperateOfficerCore)}.{nameof(TryVerifyAsync)}"
            );
        }

        await CollectAccessWay();

        PrePareVerify();

        var result = await CheckAccessPermissionAsync();

        if (result.Success) return new ExecutiveResult<ApiResult>(ReturnCode.Ok);

        var apiResultNoAccessPermission = new ApiResult(
            ReturnCode.NoPermission.ToInt(),
            false,
            result.Message
        );

        return new ExecutiveResult<ApiResult>(ReturnCode.AuthenticationFail)
        {
            Data = apiResultNoAccessPermission
        };
    }
}