using EasySoft.Core.Config.Options;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace WebApplicationTest.Options;

public class CustomStaticFileOptions : AdvanceStaticFileOptions
{
    public CustomStaticFileOptions()
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