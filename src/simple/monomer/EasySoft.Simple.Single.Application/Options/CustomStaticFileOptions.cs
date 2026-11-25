using EasySoft.UtilityTools.AspNetCore.Options;
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
        ApplicationConfigureAssist applicationConfigureAssist
    ) : base(
        applicationConfigureAssist
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