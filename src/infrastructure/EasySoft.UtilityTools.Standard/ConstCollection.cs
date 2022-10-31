using System;
using System.ComponentModel;

namespace EasySoft.UtilityTools.Standard;

/// <summary>
/// 常量集合
/// </summary>
public static class ConstCollection
{
    public const string ApplicationStartConfigMessageDivider =
        "-----------------------------------------------------------";

    public const string ApplicationStartMessageDivider =
        "===========================================================";

    public const string ApplicationStartMessageDescriptionDivider =
        "###########################################################";

    public const string ApplicationStartExtraBuilderMessageDivider =
        "==== Extra Builder Action Info ========";

    public const string ApplicationStartExtraApplicationMessageStartDivider =
        "==== Extra Applicaiton Action Info ====";

    public const string ApplicationStartExtraEndpointMessageStartDivider =
        "==== Extra Endpoint Action Info =======";

    public const string ApplicationStartExtraMvcOptionMessageStartDivider =
        "==== Extra Mvc Option Action Info =====";

    public const string JsonWebToken = "JsonWebToken";

    public const string EasyToken = "EasyToken";

    public const string SuperRoleName = "超级管理员";
    public const string SuperRoleGuidTag = "super";

    public const decimal UploadMaxSize = 10;

    public const int DefaultSupplierSettlementDelayDay = 7;

    public const string DefaultAvatarImage = "388a9b25-c3aa-440b-9dd6-5178066a3382.png";

    public const string StringIdentificationValue = "-10000";

    public static readonly DateTime ProductRankingWeightBeginTime = new(2019, 1, 1, 0, 0, 0);
    public const long ProductRankingWeightBeginUnixTime = 1546272000;

    public static readonly DateTime UserCouponRankingWeightBeginTime = new(2020, 1, 1, 0, 0, 0);
    public const long UserCouponRankingWeightBeginUnixTime = 1577808000;

    public const int IntIdentificationValue = -10000;

    public const long LongIdentificationValue = -10000;

    public const char IdentificationCodeDivider = '|';

    /// <summary>
    /// 匿名用户统一token
    /// </summary>
    public const string AnonymousToken = "anonymous";

    public static readonly DateTime DateTimeDefault = new(2010, 1, 1, 0, 0, 0);

    public const string DateTimeDefaultString = "2010-01-01 00:00:00";

    public static readonly string ApplicationStopNotificationText = "已经停止运行了哦";
    public static readonly string ApplicationPauseNotificationText = "暂时停止运行了哦，稍后再来吧哦";

    /// <summary>
    /// .net 默认日期： 0001/1/1 0:00:00
    /// </summary>
    [Description(" .net 默认日期： 0001/1/1 0:00:00")]
    public static DateTime NetDefaultDateTime = Convert.ToDateTime("0001/1/1 0:00:00");

    /// <summary>
    /// Db默认日期： 1970/1/1 0:00:00
    /// </summary>
    [Description("Db默认日期： 1970/1/1 0:00:00")]
    public static DateTime DbDefaultDateTime = Convert.ToDateTime("1970/1/1 0:00:00");

    /// <summary>
    /// 最大日期：9999/12/31 23:59:59
    /// </summary>
    [Description("最大日期：9999/12/31 23:59:59")]
    public static DateTime MaxDateTime = Convert.ToDateTime("9999/12/31 23:59:59");
}