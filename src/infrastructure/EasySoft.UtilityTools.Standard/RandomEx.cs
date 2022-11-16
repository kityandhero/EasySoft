using System.Globalization;

namespace EasySoft.UtilityTools.Standard;

public class RandomEx : Random
{
    #region Constructors

    /// <summary>
    /// Constructor
    /// </summary>
    public RandomEx()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="seed">Seed value</param>
    public RandomEx(int seed) : base(seed)
    {
    }

    #endregion

    #region Private Variables

    private static readonly Random GlobalSeed = new();

    [ThreadStatic]
    private static Random? _local;

    #endregion

    #region Static Functions

    #region ThreadSafeNext

    /// <summary>
    /// A thread safe version of a random number generation
    /// </summary>
    /// <param name="min">Minimum value</param>
    /// <param name="max">Maximum value</param>
    /// <returns>A randomly generated value</returns>
    public static int ThreadSafeNext(int min = int.MinValue, int max = int.MaxValue)
    {
        if (_local != null) return _local.Next(min, max);

        int seed;

        lock (GlobalSeed)
        {
            seed = GlobalSeed.Next();
        }

        _local = new Random(seed);

        return _local.Next(min, max);
    }

    #endregion

    #region 生成随机码

    ///  <summary>
    ///  生成随机码
    ///  </summary>
    ///  <param  name="length">随机码个数</param>
    ///  <returns></returns>
    public static string CreateRandomCode(int length)
    {
        var randomCode = string.Empty;
        //生成一定长度的验证码
        var random = new Random();
        for (var i = 0; i < length; i++)
        {
            var rand = random.Next();
            char code;
            if (rand % 3 == 0)
                code = (char)('A' + (char)(rand % 26));
            else
                code = (char)('0' + (char)(rand % 10));

            randomCode += code.ToString(CultureInfo.InvariantCulture);
        }

        return randomCode;
    }

    #endregion

    #region 生成随机码

    ///  <summary>
    ///  生成G到Z之间的任意一个字母
    ///  </summary>
    ///  <returns></returns>
    public static string CreateRandomCharFromGtoZ(out int outSeed, List<string> filter, int? seed = null)
    {
        var check = new List<string>
        {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F"
        };
        //生成一定长度的验证码

        var random = seed == null ? new Random() : new Random((int)seed);

        outSeed = random.Next();

        var code = outSeed % 3 == 0
            ? ((char)('A' + (char)(outSeed % 26))).ToString(CultureInfo.InvariantCulture).ToUpper()
            : CreateRandomCharFromGtoZ(out outSeed, filter, outSeed);

        if (check.Contains(code)) code = CreateRandomCharFromGtoZ(out outSeed, filter, outSeed);

        if (filter.Count <= 0) return code;

        if (filter.Contains(code)) code = CreateRandomCharFromGtoZ(out outSeed, filter, outSeed);

        return code;
    }

    #endregion

    #endregion
}