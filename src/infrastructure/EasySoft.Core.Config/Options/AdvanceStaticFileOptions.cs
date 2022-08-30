using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace EasySoft.Core.Config.Options;

public abstract class AdvanceStaticFileOptions : StaticFileOptions
{
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