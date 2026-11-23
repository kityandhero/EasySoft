using EasySoft.Core.Infrastructure.Options;
using Microsoft.AspNetCore.StaticFiles;

namespace EasySoft.Simple.Single.Application.Options;

/// <summary>
/// CustomStaticFileOptions
/// </summary>
public class CustomStaticFileOptions : AdvanceStaticFileOptions
{
    /// <summary>
    /// CustomStaticFileOptions
    /// </summary>
    public CustomStaticFileOptions(
        InfrastructureConfigureAssist infrastructureConfigureAssist
    ) : base(
        infrastructureConfigureAssist
    )
    {
        var provider = new FileExtensionContentTypeProvider
        {
            Mappings =
            {
                [".properties"] = "application/octet-stream"
            }
        };

        RequestPath = "";
        ContentTypeProvider = provider;
    }
}