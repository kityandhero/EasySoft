using System;
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SkiaSharp;

namespace EasySoft.UtilityTools.ExtensionMethods;

// ImageSharp is a new, fully featured, fully managed, cross-platform, 2D graphics library.
// Designed to simplify image processing, ImageSharp brings you an incredibly powerful yet beautifully simple API.
// https://docs.sixlabors.com/

public static class SKImageExtensions
{
    public static void Save(this SKImage image, string targetPath, int width, int height)
    {
        if (string.IsNullOrWhiteSpace(targetPath))
        {
            throw new Exception("targetPath image uri disallow null or empty");
        }

        // using (var img = SKImage.FromBitmap(image2d))
        // {
        //     using (var p = img.Encode(SKEncodedImageFormat.Png, 100))
        //     {
        //         return p.ToArray();
        //     }
        // }

        try
        {
            if (!string.IsNullOrEmpty(folder))
            {
                folderDirectory = new File(picturesDirectory, folder);
                folderDirectory.Mkdirs();
            }

            using (File bitmapFile = new File(folderDirectory, filename))
            {
                bitmapFile.CreateNewFile();

                using (FileOutputStream outputStream = new FileOutputStream(bitmapFile))
                {
                    await outputStream.WriteAsync(data);
                }

                // Make sure it shows up in the Photos gallery promptly.
                MediaScannerConnection.ScanFile(MainActivity.Instance,
                    new string[] { bitmapFile.Path },
                    new string[] { "image/png", "image/jpeg" }, null);
            }
        }
        catch
        {
            return false;
        }
    }

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