namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// FormatAssist
/// </summary>
public static class FormatAssist
{
    /// <summary>
    /// FormatIntToString
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public static string FormatIntToString(int count)
    {
        string result;

        if (count > 0)
        {
            if (count >= 10000)
            {
                if (count < 100000)
                {
                    var c = Math.Round((double)count / 100, MidpointRounding.AwayFromZero);

                    result = (c / 100).ToString("0.00万");
                }
                else
                {
                    var c = Math.Round((double)count / 100, MidpointRounding.AwayFromZero);

                    result = (c / 100).ToString("0.00万");
                }
            }
            else
            {
                result = count.ToString();
            }
        }
        else
        {
            result = "0";
        }

        return result;
    }
}