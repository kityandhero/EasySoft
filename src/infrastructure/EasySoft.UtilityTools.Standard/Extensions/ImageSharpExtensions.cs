namespace EasySoft.UtilityTools.Standard.Extensions;

// ImageSharp is a new, fully featured, fully managed, cross-platform, 2D graphics library.
// Designed to simplify image processing, ImageSharp brings you an incredibly powerful yet beautifully simple API.
// https://docs.sixlabors.com/

/// <summary>
/// ImageSharpExtensions
/// </summary>
public static class ImageSharpExtensions
{
    /// <summary>
    /// Resize
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="stream"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="format"></param>
    /// <exception cref="Exception"></exception>
    public static void Resize(this string uri, Stream stream, int width, int height, IImageFormat format)
    {
        if (string.IsNullOrWhiteSpace(uri)) throw new Exception("mutate image uri disallow null or empty");

        using var image = Image.Load(uri);

        image.Mutate(x => x.Resize(width / 2, height / 2));

        image.Save(stream, format);
    }

    /// <summary>
    /// ResizeAsync
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="stream"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="format"></param>
    /// <exception cref="Exception"></exception>
    public static async Task ResizeAsync(this string uri, Stream stream, int width, int height, IImageFormat format)
    {
        if (string.IsNullOrWhiteSpace(uri)) throw new Exception("mutate image uri disallow null or empty");

        using var image = await Image.LoadAsync(uri);

        image.Mutate(x => x.Resize(width / 2, height / 2));

        await image.SaveAsync(stream, format);
    }
}