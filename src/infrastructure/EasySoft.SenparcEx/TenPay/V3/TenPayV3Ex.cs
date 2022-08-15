using System.Text;
using Senparc.CO2NET;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPay.V3;

namespace EasySoft.SenparcEx.TenPay.V3
{
    public static class TenPayV3Ex
    {
        public static TransfersResult Transfers(
            IServiceProvider serviceProvider,
            TenPayV3TransfersRequestData dataInfo,
            int timeOut = Config.TIME_OUT
        )
        {
            var urlFormat = ReturnPayApiUrl(
                Senparc.Weixin.Config.TenPayV3Host + "/{0}mmpaymkttransfers/promotion/transfers"
            );
            var data = dataInfo.PackageRequestHandler.ParseXML();

            var responseContent = CertPost_NetCore(
                serviceProvider,
                dataInfo.MchId,
                dataInfo.SubMchId,
                data,
                urlFormat,
                timeOut
            );

            return new TransfersResult(responseContent);
        }

        /// <summary>
        /// 带证书提交
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="mchId">商户号</param>
        /// <param name="subMchId">子商户号，如果没有则填 null 或空字符串</param>
        /// <param name="data">数据</param>
        /// <param name="url">Url</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        private static string CertPost_NetCore(
            IServiceProvider serviceProvider,
            string mchId,
            string subMchId,
            string data,
            string url,
            int timeOut = Config.TIME_OUT
        )
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);

            using (var ms = new MemoryStream(dataBytes))
            {
                var certName = TenPayHelper.GetRegisterKey(mchId, subMchId);
                var responseContent = RequestUtility.HttpPost(
                    serviceProvider,
                    url,
                    postStream: ms,
                    certName: certName,
                    timeOut: timeOut
                );

                return responseContent;
            }
        }

        /// <summary>
        /// 返回可用的微信支付地址（自动判断是否使用沙箱）
        /// </summary>
        /// <param name="urlFormat">如：<code>https://api.mch.weixin.qq.com/{0}pay/unifiedorder</code></param>
        /// <returns></returns>
        private static string ReturnPayApiUrl(string urlFormat)
        {
            return string.Format(urlFormat, Senparc.Weixin.Config.UseSandBoxPay ? "sandboxnew/" : "");
        }
    }
}