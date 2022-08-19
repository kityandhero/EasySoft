using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace WebApplicationTest.Options;

public class CustomStaticFileOptions : StaticFileOptions
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

        FileProvider = new PhysicalFileProvider(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "/"
            )
        );
        RequestPath = "";
        ContentTypeProvider = provider;
    }
}