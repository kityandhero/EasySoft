using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.IdentityVerification.AccessControl;

public static class AccessAssist
{
    /// <summary>
    /// GetFromSimpleInfo
    /// </summary>
    /// <param name="info">  </param>
    /// <param name="secret"></param>
    /// <returns></returns>
    public static AccessKey? GetFromSimpleInfoWithSecret(string info, Secret secret)
    {
        AccessKey? result = null;

        var a = info.Split('_');

        if (a.Length != 2)
        {
            return result;
        }

        if (!string.IsNullOrWhiteSpace(a[0]) && a[1].In("0", "1"))
        {
            result = new AccessKey(secret)
            {
                IdentificationCode = Convert.ToString(a[0]),
                Effective = Convert.ToInt32(a[1])
            };
        }

        return result;
    }
}