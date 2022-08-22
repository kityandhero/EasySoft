using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.AspNetCore.Http;

namespace EasySoft.UtilityTools.Core.Standard.Assists
{
    /// <summary>
    /// IP辅助类
    /// </summary>
    public static class IPAssist
    {
        /// <summary>
        /// 判断IP地址是否为内网IP地址
        /// </summary>
        /// <param name="address">IP地址字符串</param>
        /// <returns></returns>
        public static bool IsInnerAddress(string address)
        {
            var ipNum = GetIpNum(address);

            /*
            私有IP：A类 10.0.0.0-10.255.255.255
            B类 172.16.0.0-172.31.255.255
            C类 192.168.0.0-192.168.255.255
            当然，还有127这个网段是环回地址
            */
            var aBegin = GetIpNum("10.0.0.0");
            var aEnd = GetIpNum("10.255.255.255");
            var bBegin = GetIpNum("172.16.0.0");
            var bEnd = GetIpNum("172.31.255.255");
            var cBegin = GetIpNum("192.168.0.0");
            var cEnd = GetIpNum("192.168.255.255");

            var isInnerIp = IsInner(ipNum, aBegin, aEnd) || IsInner(ipNum, bBegin, bEnd) ||
                            IsInner(ipNum, cBegin, cEnd) || address.Equals("127.0.0.1");
            return isInnerIp;
        }

        /// <summary>
        /// 把IP地址转换为Long型数字
        /// </summary>
        /// <param name="address">IP地址字符串</param>
        /// <returns></returns>
        public static long GetIpNum(this string address)
        {
            var ip = address.Split('.');
            long a = int.Parse(ip[0]);
            long b = int.Parse(ip[1]);
            long c = int.Parse(ip[2]);
            long d = int.Parse(ip[3]);

            var ipNum = a * 256 * 256 * 256 + b * 256 * 256 + c * 256 + d;

            return ipNum;
        }

        /// <summary>
        /// 判断用户IP地址转换为Long型后是否在内网IP地址所在范围
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="begin"></param>
        /// <param name="end">  </param>
        /// <returns></returns>
        private static bool CurrentIsInner(HttpContext httpContext, long begin, long end)
        {
            var ip = GetCurrentIpNum(httpContext);

            return (ip >= begin) && (ip <= end);
        }

        /// <summary>
        /// 判断当前IP地址是否为内网IP地址
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentIpNum(HttpContext httpContext)
        {
            return GetIpNum(httpContext.GetCurrentAddress());
        }

        /// <summary>
        /// 获取当前使用的IP
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            var ip = NetworkInterface.GetAllNetworkInterfaces()
                .Select(p => p.GetIPProperties())
                .SelectMany(p => p.UnicastAddresses)
                .FirstOrDefault(p =>
                    p.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(p.Address)
                )?.Address
                .ToString() ?? "";

            return ip;
        }

        /// <summary>
        /// 判断用户IP地址转换为Long型后是否在内网IP地址所在范围
        /// </summary>
        /// <param name="ip">   </param>
        /// <param name="begin"></param>
        /// <param name="end">  </param>
        /// <returns></returns>
        private static bool IsInner(long ip, long begin, long end)
        {
            return (ip >= begin) && (ip <= end);
        }

        static string IpReplace(string ip)
        {
            //::ffff:
            //::ffff:192.168.2.131 这种IP处理
            if (ip.Contains("::ffff:"))
            {
                ip = ip.Replace("::ffff:", "");
            }

            return ip;
        }

        /// <summary>
        /// 获取本机主DNS
        /// </summary>
        /// <returns></returns>
        public static string GetPrimaryDns()
        {
            var result = RunApp("nslookup", "");
            var m = Regex.Match(result, @"\d+\.\d+\.\d+\.\d+");

            return m.Success ? m.Value : "";
        }

        /// <summary>
        /// 运行一个控制台程序并返回其输出参数。
        /// </summary>
        /// <param name="filename"> 程序名</param>
        /// <param name="arguments">输入参数</param>
        /// <returns></returns>
        public static string RunApp(string filename, string arguments)
        {
            try
            {
                var proc = new Process
                {
                    StartInfo =
                    {
                        FileName = filename,
                        CreateNoWindow = true,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        UseShellExecute = false
                    }
                };
                proc.Start();

                using (var sr = new StreamReader(proc.StandardOutput.BaseStream, Encoding.Default))
                {
                    //string txt = sr.ReadToEnd();
                    //sr.Close();
                    //if (recordLog)
                    //{
                    //    Trace.WriteLine(txt);
                    //}
                    //if (!proc.HasExited)
                    //{
                    //    proc.Kill();
                    //}
                    //上面标记的是原文，下面是我自己调试错误后自行修改的

                    //貌似调用系统的nslookup还未返回数据或者数据未编码完成，程序就已经跳过直接执行
                    Thread.Sleep(100);

                    //txt = sr.ReadToEnd()了，导致返回的数据为空，故睡眠令硬件反应
                    //在无参数调用nslookup后，可以继续输入命令继续操作，如果进程未停止就直接执行
                    if (!proc.HasExited)
                    {
                        //txt = sr.ReadToEnd()程序就在等待输入，而且又无法输入，直接掐住无法继续运行
                        proc.Kill();
                    }

                    var txt = sr.ReadToEnd();
                    sr.Close();

                    return txt;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}