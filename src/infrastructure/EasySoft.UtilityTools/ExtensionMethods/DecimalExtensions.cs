using System;
using System.Globalization;

namespace EasySoft.UtilityTools.ExtensionMethods;

public class DecimalExtensions
{
    /// <summary> 
    /// 转换人民币大小金额 
    /// </summary> 
    /// <param name="num">金额</param> 
    /// <returns>返回大写形式</returns> 
    public static string ConvertToChinaYuan(decimal num)
    {
        const string str1 = "零壹贰叁肆伍陆柒捌玖"; //0-9所对应的汉字 
        var str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字   
        var str5 = ""; //人民币大写金额形式 
        int i; //循环变量 
        var ch2 = "";
        var zero = 0; //用来计算连续的零值是几个 

        num = Math.Round(Math.Abs(num), 2); //将num取绝对值并四舍五入取2位小数 

        var str4 = ((long)(num * 100)).ToString(CultureInfo.InvariantCulture);
        var j = str4.Length;

        if (j > 15)
        {
            return "溢出";
        }

        str2 = str2.Substring(15 - j); //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

        //循环取出每一位需要转换的值 
        for (i = 0; i < j; i++)
        {
            var str3 = str4.Substring(i, 1); //从原num值中取出的值 
            var temp = Convert.ToInt32(str3); //从原num值中取出的值 
            string ch1; //数字的汉语读法 

            if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
            {
                //当所取位数不为元、万、亿、万亿上的数字时 
                if (str3 == "0")
                {
                    ch1 = "";
                    ch2 = "";
                    zero = zero + 1;
                }
                else
                {
                    if (str3 != "0" && zero != 0)
                    {
                        ch1 = string.Concat("零", str1.AsSpan(temp * 1, 1));
                        ch2 = str2.Substring(i, 1);
                        zero = 0;
                    }
                    else
                    {
                        ch1 = str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        zero = 0;
                    }
                }
            }
            else
            {
                //该位是万亿，亿，万，元位等关键位 
                if (str3 != "0" && zero != 0)
                {
                    ch1 = "零" + str1.Substring(temp * 1, 1);
                    ch2 = str2.Substring(i, 1);
                    zero = 0;
                }
                else
                {
                    if (str3 != "0" && zero == 0)
                    {
                        ch1 = str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        zero = 0;
                    }
                    else
                    {
                        if (str3 == "0" && zero >= 3)
                        {
                            ch1 = "";
                            ch2 = "";
                            zero = zero + 1;
                        }
                        else
                        {
                            if (j >= 11)
                            {
                                ch1 = "";
                                zero = zero + 1;
                            }
                            else
                            {
                                ch1 = "";
                                ch2 = str2.Substring(i, 1);
                                zero = zero + 1;
                            }
                        }
                    }
                }
            }

            if (i == (j - 11) || i == (j - 3))
            {
                //如果该位是亿位或元位，则必须写上 
                ch2 = str2.Substring(i, 1);
            }

            str5 = str5 + ch1 + ch2;

            if (i == j - 1 && str3 == "0")
            {
                //最后一位（分）为0时，加上“整” 
                str5 = str5 + '整';
            }
        }

        if (num == 0)
        {
            str5 = "零元整";
        }

        return str5;
    }

    /// <summary> 
    /// 一个重载，将字符串先转换成数字在调用(decimal num) 
    /// </summary>
    /// <returns></returns> 
    public static string ConvertToChinaYuan(string numberString)
    {
        try
        {
            var num = Convert.ToDecimal(numberString);
            return ConvertToChinaYuan(num);
        }
        catch
        {
            return "非数字形式！";
        }
    }
}