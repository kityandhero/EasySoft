using EasySoft.Core.AppSecurityServer.Core.Entities;
using EasySoft.Core.AppSecurityServer.Core.Services.Implements;
using EasySoft.Core.AppSecurityServer.Core.Services.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Assist;

/// <summary>
/// PermissionServerAssist
/// </summary>
public static class AppSecurityServerAssist
{
    /// <summary>
    /// Init
    /// </summary>
    public static void Init(bool embedMode)
    {
        AppSecurityServerConfigure.EmbedMode = embedMode;
        ContextConfigure.AddEntityConfigureAssembly(typeof(AppSecurity).Assembly);

        BusinessServiceConfigure.AddBusinessServiceInterfaceAssembly(typeof(IAppSecurityService).Assembly);
        BusinessServiceConfigure.AddBusinessServiceImplementationAssembly(typeof(AppSecurityService).Assembly);
    }
}