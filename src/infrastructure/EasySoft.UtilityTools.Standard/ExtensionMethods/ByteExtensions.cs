using System;
using System.Text;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

public static class ByteExtensions
{
    #region ToString

    /// <summary>
    /// Converts a byte array into a base 64 string
    /// </summary>
    /// <param name="input">Input array</param>
    /// <param name="count">Number of bytes starting at the index to convert (use -1 for the entire array starting at the index)</param>
    /// <param name="index">Index to start at</param>
    /// <param name="options">Base 64 formatting options</param>
    /// <returns>The equivalent byte array in a base 64 string</returns>
    public static string ToString(this byte[] input, Base64FormattingOptions options, int index = 0, int count = -1)
    {
        if (count == -1)
        {
            count = input.Length - index;
        }

        return Convert.ToBase64String(input, index, count, options);
    }

    /// <summary>
    /// Converts a byte array to a string
    /// </summary>
    /// <param name="input">input array</param>
    /// <param name="encodingUsing">The type of encoding the string is using (defaults to UTF8)</param>
    /// <param name="count">Number of bytes starting at the index to convert (use -1 for the entire array starting at the index)</param>
    /// <param name="index">Index to start at</param>
    /// <returns>string of the byte array</returns>
    public static string ToString(this byte[] input, Encoding encodingUsing, int index = 0, int count = -1)
    {
        if (count == -1)
        {
            count = input.Length - index;
        }

        return encodingUsing.Check(new UTF8Encoding())?.GetString(input, index, count) ?? "";
    }

    #endregion
}