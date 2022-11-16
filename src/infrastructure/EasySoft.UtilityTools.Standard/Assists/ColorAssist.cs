using System.Globalization;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// ColorAssist
/// </summary>
public static class ColorAssist
{
    /// <summary>
    /// Colors
    /// </summary>
    public static readonly List<Color> Colors = new()
    {
        Color.AliceBlue,
        Color.PaleGreen,
        Color.PaleGoldenrod,
        Color.Orchid,
        Color.OrangeRed,
        Color.Orange,
        Color.OliveDrab,
        Color.Olive,
        Color.OldLace,
        Color.Navy,
        Color.NavajoWhite,
        Color.Moccasin,
        Color.MistyRose,
        Color.MintCream,
        Color.MidnightBlue,
        Color.MediumVioletRed,
        Color.MediumTurquoise,
        Color.MediumSpringGreen,
        Color.LightSlateGray,
        Color.LightSteelBlue,
        Color.LightYellow,
        Color.Lime,
        Color.LimeGreen,
        Color.Linen,
        Color.PaleTurquoise,
        Color.Magenta,
        Color.MediumAquamarine,
        Color.MediumBlue,
        Color.MediumOrchid,
        Color.MediumPurple,
        Color.MediumSeaGreen,
        Color.MediumSlateBlue,
        Color.Maroon,
        Color.PaleVioletRed,
        Color.PapayaWhip,
        Color.PeachPuff,
        Color.Snow,
        Color.SpringGreen,
        Color.SteelBlue,
        Color.Tan,
        Color.Teal,
        Color.Thistle,
        Color.SlateGray,
        Color.Tomato,
        Color.Violet,
        Color.Wheat,
        Color.White,
        Color.WhiteSmoke,
        Color.Yellow,
        Color.YellowGreen,
        Color.Turquoise,
        Color.LightSkyBlue,
        Color.SlateBlue,
        Color.Silver,
        Color.Peru,
        Color.Pink,
        Color.Plum,
        Color.PowderBlue,
        Color.Purple,
        Color.Red,
        Color.SkyBlue,
        Color.RosyBrown,
        Color.SaddleBrown,
        Color.Salmon,
        Color.SandyBrown,
        Color.SeaGreen,
        Color.SeaShell,
        Color.Sienna,
        Color.RoyalBlue,
        Color.LightSeaGreen,
        Color.LightSalmon,
        Color.LightPink,
        Color.Crimson,
        Color.Cyan,
        Color.DarkBlue,
        Color.DarkCyan,
        Color.DarkGoldenrod,
        Color.DarkGray,
        Color.Cornsilk,
        Color.DarkGreen,
        Color.DarkMagenta,
        Color.DarkOliveGreen,
        Color.DarkOrange,
        Color.DarkOrchid,
        Color.DarkRed,
        Color.DarkSalmon,
        Color.DarkKhaki,
        Color.DarkSeaGreen,
        Color.CornflowerBlue,
        Color.Chocolate,
        Color.AntiqueWhite,
        Color.Aqua,
        Color.Aquamarine,
        Color.Azure,
        Color.Beige,
        Color.Bisque,
        Color.Coral,
        Color.Black,
        Color.Blue,
        Color.BlueViolet,
        Color.Brown,
        Color.BurlyWood,
        Color.CadetBlue,
        Color.Chartreuse,
        Color.BlanchedAlmond,
        Color.Transparent,
        Color.DarkSlateBlue,
        Color.DarkTurquoise,
        Color.IndianRed,
        Color.Indigo,
        Color.Ivory,
        Color.Khaki,
        Color.Lavender,
        Color.LavenderBlush,
        Color.HotPink,
        Color.LawnGreen,
        Color.LightBlue,
        Color.LightCoral,
        Color.LightCyan,
        Color.LightGoldenrodYellow,
        Color.LightGray,
        Color.LightGreen,
        Color.LemonChiffon,
        Color.DarkSlateGray,
        Color.Honeydew,
        Color.Green,
        Color.DarkViolet,
        Color.DeepPink,
        Color.DeepSkyBlue,
        Color.DimGray,
        Color.DodgerBlue,
        Color.Firebrick,
        Color.GreenYellow,
        Color.FloralWhite,
        Color.Fuchsia,
        Color.Gainsboro,
        Color.GhostWhite,
        Color.Gold,
        Color.Goldenrod,
        Color.Gray,
        Color.ForestGreen
    };

    /// <summary>
    /// FontColors
    /// </summary>
    public static readonly List<Color> FontColors = new()
    {
        Color.Aqua,
        Color.Black,
        Color.Blue,
        Color.BlueViolet,
        Color.Brown,
        Color.Chartreuse,
        Color.Chocolate,
        Color.Crimson,
        Color.Cyan,
        Color.DarkBlue,
        Color.DarkCyan,
        Color.DarkGreen,
        Color.DarkMagenta,
        Color.DarkSlateBlue,
        Color.DarkGoldenrod,
        Color.DarkViolet,
        Color.DeepPink,
        Color.DeepSkyBlue,
        Color.DodgerBlue,
        Color.Firebrick,
        Color.ForestGreen,
        Color.Fuchsia,
        Color.Green,
        Color.Gold,
        Color.GreenYellow,
        Color.HotPink,
        Color.IndianRed,
        Color.LawnGreen,
        Color.LightCoral,
        Color.LightSeaGreen,
        Color.Lime,
        Color.LimeGreen,
        Color.Magenta,
        Color.Maroon,
        Color.MediumAquamarine,
        Color.MediumBlue,
        Color.MediumPurple,
        Color.OrangeRed,
        Color.Orange,
        Color.Orchid,
        Color.PaleGoldenrod,
        Color.Peru,
        Color.PowderBlue,
        Color.Purple,
        Color.Red,
        Color.RosyBrown,
        Color.RoyalBlue,
        Color.SandyBrown,
        Color.Salmon,
        Color.Sienna,
        Color.SeaGreen,
        Color.SlateBlue,
        Color.SpringGreen,
        Color.Teal,
        Color.SteelBlue,
        Color.Tomato,
        Color.Turquoise,
        Color.Violet,
        Color.YellowGreen
    };

    /// <summary>
    /// 获取随机颜色
    /// </summary>
    /// <returns></returns>
    public static Color GetRandomColor()
    {
        var random = new Random();

        return Colors[random.Next(Colors.Count)];
    }

    /// <summary>
    /// 获取随机颜色
    /// </summary>
    /// <returns></returns>
    public static Color GetFontRandomColor()
    {
        var random = new Random();

        return FontColors[random.Next(FontColors.Count)];
    }

    /// <summary>
    /// 获取随机颜色
    /// </summary>
    /// <returns></returns>
    public static Color GetRandomColorRgba()
    {
        var random = new Random();

        var red = random.Next(0, 255);
        var green = random.Next(0, 255);
        var blue = random.Next(0, 255);
        var alpha = random.Next(0, 255);

        return Color.FromRgba(
            Convert.ToByte(red),
            Convert.ToByte(green),
            Convert.ToByte(blue),
            Convert.ToByte(alpha)
        );
    }

    /// <summary>
    /// HexToColor
    /// </summary>
    /// <param name="hexColor"></param>
    /// <returns></returns>
    public static Color HexToColor(string hexColor)
    {
        //Remove # if present
        if (hexColor.IndexOf('#') != -1) hexColor = hexColor.Replace("#", "");

        var red = 0;
        var green = 0;
        var blue = 0;

        if (hexColor.Length == 6)
        {
            //#RRGGBB
            red = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            green = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            blue = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
        }
        else if (hexColor.Length == 3)
        {
            //#RGB
            red = int.Parse(hexColor[0].ToString() + hexColor[0].ToString(), NumberStyles.AllowHexSpecifier);
            green = int.Parse(hexColor[1].ToString() + hexColor[1].ToString(), NumberStyles.AllowHexSpecifier);
            blue = int.Parse(hexColor[2].ToString() + hexColor[2].ToString(), NumberStyles.AllowHexSpecifier);
        }

        return Color.FromRgb(
            Convert.ToByte(red),
            Convert.ToByte(green),
            Convert.ToByte(blue)
        );
    }
}