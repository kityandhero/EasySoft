using EasySoft.Core.AutoFac.ExtensionMethods;
using EasySoft.Core.PrepareStartWork.ExtensionMethods;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Web.Framework.BuilderAssists;

public static class WebApplicationBuilderAssist
{
    public static WebApplicationBuilder CreateBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = args
        });

        builder.UseAdvanceAutoFac();

        builder.UseCovertInjection();

        return builder;
    }
}