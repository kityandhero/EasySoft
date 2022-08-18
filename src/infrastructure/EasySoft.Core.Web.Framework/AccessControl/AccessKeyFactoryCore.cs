namespace EasySoft.Core.Web.Framework.AccessControl
{
    public static class AccessKeyFactoryCore
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static AccessKey Create(Secret secret)
        {
            var ak = new AccessKey(secret);

            return ak;
        }
    }
}