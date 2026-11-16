using EasySoft.Core.Infrastructure.Assists;

namespace EasySoft.Core.Infrastructure.Options;

/// <summary>
/// AdvanceStaticFileOptions
/// </summary>
public abstract class AdvanceStaticFileOptions : StaticFileOptions
{
    #region Fields

    private readonly InfrastructureConfigureAssist _infrastructureConfigureAssist;

    #endregion

    /// <summary>
    /// AdvanceStaticFileOptions
    /// </summary>
    protected AdvanceStaticFileOptions(InfrastructureConfigureAssist infrastructureConfigureAssist)
    {
        _infrastructureConfigureAssist = infrastructureConfigureAssist;

        var webRootPath = _infrastructureConfigureAssist.GetWebRootPath().Value;

        if (!string.IsNullOrWhiteSpace(webRootPath))
        {
            FileProvider = new PhysicalFileProvider(
                Directory.GetCurrentDirectory().Combine(webRootPath)
            );
        }
    }
}