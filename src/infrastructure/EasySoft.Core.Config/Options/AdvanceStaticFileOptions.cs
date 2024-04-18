using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Config.Options;

/// <summary>
/// AdvanceStaticFileOptions
/// </summary>
public abstract class AdvanceStaticFileOptions : StaticFileOptions
{
    /// <summary>
    /// AdvanceStaticFileOptions
    /// </summary>
    protected AdvanceStaticFileOptions()
    {
        if (!string.IsNullOrWhiteSpace(GeneralConfigAssist.GetWebRootPath()))
        {
            FileProvider = new PhysicalFileProvider(
                Directory.GetCurrentDirectory().Combine(GeneralConfigAssist.GetWebRootPath())
            );
        }
    }
}