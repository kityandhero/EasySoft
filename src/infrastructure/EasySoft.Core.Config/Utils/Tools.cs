﻿using System.Resources;
using EasySoft.Core.Config.ConfigCollection;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Embedded;

namespace EasySoft.Core.Config.Utils;

public static class Tools
{
    public static string GetConfigureDirectory()
    {
        var configureFolderPath = $"{AppContextAssist.GetBaseDirectory()}/configures/";

        return configureFolderPath;
    }

    internal static string GetEmbeddedResourceFileContent(string path)
    {
        var personEmbeddedFileProvider = new EmbeddedFileProvider(
            typeof(Tools).Assembly,
            typeof(ConstCollection).Namespace
        );

        var f = personEmbeddedFileProvider.GetFileInfo(path);

        return f.CreateReadStream().ReadAll();
    }
}