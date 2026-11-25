using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.UtilityTools.AspNetCore.Options;

/// <summary>
/// AdvanceStaticFileOptions
/// </summary>
public abstract class AdvanceStaticFileOptions : StaticFileOptions
{
    #region Fields

    private readonly ApplicationConfigureAssist _applicationConfigureAssist;

    #endregion

    /// <summary>
    /// AdvanceStaticFileOptions
    /// </summary>
    protected AdvanceStaticFileOptions(ApplicationConfigureAssist applicationConfigureAssist)
    {
        _applicationConfigureAssist = applicationConfigureAssist;

        var webRootPath = _applicationConfigureAssist.GetWebRootPath().Value;

        if (!string.IsNullOrWhiteSpace(webRootPath))
        {
            FileProvider = new PhysicalFileProvider(
                Directory.GetCurrentDirectory().Combine(webRootPath)
            );
        }
    }
}