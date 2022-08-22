using System;
using System.Security.Cryptography;
using System.Text;

namespace EasySoft.UtilityTools.Standard.Encryption
{
    public static class Md5Assist
    {
        public static string ToMd5(string source)
        {
            using var md5 = MD5.Create();

            var result = md5.ComputeHash(Encoding.ASCII.GetBytes(source));
            var strResult = BitConverter.ToString(result);

            return strResult.Replace("-", "").ToLower();
        }
    }
}