using System;
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;

namespace EasySoft.UtilityTools.ExtensionMethods;

// ImageSharp is a new, fully featured, fully managed, cross-platform, 2D graphics library.
// Designed to simplify image processing, ImageSharp brings you an incredibly powerful yet beautifully simple API.
// https://docs.sixlabors.com/

public static class ImageSharpExtensions
{
    public static void Resize(this string uri, Stream stream, int width, int height, IImageFormat format)
    {
        if (string.IsNullOrWhiteSpace(uri))
        {
            throw new Exception("mutate image uri disallow null or empty");
        }

        using var image = Image.Load(uri);

        image.Mutate(x => x.Resize(width / 2, height / 2));

        image.Save(stream, format);
    }

    public static async Task ResizeAsync(this string uri, Stream stream, int width, int height, IImageFormat format)
    {
        if (string.IsNullOrWhiteSpace(uri))
        {
            throw new Exception("mutate image uri disallow null or empty");
        }

        using var image = await Image.LoadAsync(uri);

        image.Mutate(x => x.Resize(width / 2, height / 2));

        await image.SaveAsync(stream, format);
    }
}