using System;
using EasySoft.UtilityTools.Core.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

public static class ContentConfigurationExtensions
{
    // public static IContentProvider GetContentProvider(this IConfigurationBuilder builder)
    // {
    //     if (builder == null)
    //     {
    //         throw new ArgumentNullException(nameof(builder));
    //     }
    //
    //     if (builder.Properties.TryGetValue(ContentProviderKey, out object provider))
    //     {
    //         return provider as IContentProvider;
    //     }
    //
    //     return new ContentProvider(AppContext.BaseDirectory ?? string.Empty);
    // }
}