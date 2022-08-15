namespace Framework.AccessControl
{
    public class AccessKey
    {
        /// <summary>
        /// UserId
        /// </summary>
        public string IdentificationCode { get; set; }

        /// <summary>
        /// Enabled
        /// </summary>
        public int Effective { get; set; }

        /// <summary>
        /// 安全类实例
        /// </summary>
        private Secret Secret { get; }

        public AccessKey(Secret secret)
        {
            Secret = secret;
            IdentificationCode = "";
        }

        /// <summary>
        /// GetSimpleInfo
        /// </summary>
        /// <returns></returns>
        private string GetSimpleInfo()
        {
            return IdentificationCode + "_" + Effective;
        }

        public string GetSecret(int expirationMinute = 120)
        {
            var v = Secret.EncryptWithExpirationTime(
                GetSimpleInfo(),
                new TimeSpan(TimeSpan.TicksPerMinute * expirationMinute)
            );

            return v;
        }
    }
}