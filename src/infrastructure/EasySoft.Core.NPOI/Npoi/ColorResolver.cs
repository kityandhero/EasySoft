using EasySoft.Core.NPOI.Core;

namespace EasySoft.Core.NPOI.Npoi;

/// <summary>
/// 颜色转换
/// </summary>
public static class ColorResolver
{
    /// <summary>
    /// 转换
    /// </summary>
    /// <param name="color">颜色枚举</param>
    public static short Resolve(Color? color)
    {
        if (color == null)
        {
            return 0x3e;
        }

        return color switch
        {
            Color.Aqua => 0x31,
            Color.Black => 0x8,
            Color.Blue => 0xc,
            Color.BlueGrey => 0x36,
            Color.BrightGreen => 0xb,
            Color.Brown => 0x3c,
            Color.Coral => 0x1d,
            Color.CornflowerBlue => 0x1f,
            Color.DarkBlue => 0x12,
            Color.DarkGreen => 0x3a,
            Color.DarkRed => 0x10,
            Color.DarkTeal => 0x38,
            Color.DarkYellow => 0x13,
            Color.Gold => 0x33,
            Color.Green => 0xb,
            Color.Grey25Percent => 0x16,
            Color.Grey40Percent => 0x37,
            Color.Grey50Percent => 0x17,
            Color.Grey80Percent => 0x3f,
            Color.Indigo => 0x3e,
            Color.Lavender => 0x2e,
            Color.LemonChiffon => 0x1a,
            Color.LightBlue => 0x30,
            Color.LightCornflowerBlue => 0x1f,
            Color.LightGreen => 0x2a,
            Color.LightOrange => 0x34,
            Color.LightTurquoise => 0x29,
            Color.LightYellow => 0x2b,
            Color.Lime => 0x32,
            Color.Maroon => 0x19,
            Color.OliveGreen => 0x3b,
            Color.Orange => 0x35,
            Color.Orchid => 0x1c,
            Color.PaleBlue => 0x2c,
            Color.Pink => 0xe,
            Color.Plum => 0x3d,
            Color.Red => 0xa,
            Color.Rose => 0x2d,
            Color.RoyalBlue => 0x1e,
            Color.SeaGreen => 0x39,
            Color.SkyBlue => 0x28,
            Color.Tan => 0x2f,
            Color.Teal => 0x15,
            Color.Turquoise => 0xf,
            Color.Violet => 0x14,
            Color.White => 0x9,
            Color.Yellow => 0xd,
            _ => 0x3e
        };
    }
}