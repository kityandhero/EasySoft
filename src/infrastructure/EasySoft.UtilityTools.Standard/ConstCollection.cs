namespace EasySoft.UtilityTools.Standard;

/// <summary>
/// 常量集合
/// </summary>
public static class ConstCollection
{
    /// <summary>
    /// PromptMessageDivider
    /// </summary>
    public const string PromptMessageDivider =
        "-----------------------------------------------------------";

    /// <summary>
    /// ApplicationStartConfigMessageDivider
    /// </summary>
    public const string ApplicationStartConfigMessageDivider =
        "-----------------------------------------------------------";

    /// <summary>
    /// ApplicationStartMessageDivider
    /// </summary>
    public const string ApplicationStartMessageDivider =
        "===========================================================";

    /// <summary>
    /// ApplicationStartMessageDescriptionDivider
    /// </summary>
    public const string ApplicationStartMessageDescriptionDivider =
        "***********************************************************";

    /// <summary>
    /// ApplicationStartExtraBuilderMessageDivider
    /// </summary>
    public const string ApplicationStartExtraBuilderMessageDivider =
        "==== Extra Builder Action Info ========";

    /// <summary>
    /// ApplicationStartExtraApplicationMessageStartDivider
    /// </summary>
    public const string ApplicationStartExtraApplicationMessageStartDivider =
        "==== Extra Applicaiton Action Info ====";

    /// <summary>
    /// ApplicationStartExtraEndpointMessageStartDivider
    /// </summary>
    public const string ApplicationStartExtraEndpointMessageStartDivider =
        "==== Extra Endpoint Action Info =======";

    /// <summary>
    /// ApplicationStartExtraMvcOptionMessageStartDivider
    /// </summary>
    public const string ApplicationStartExtraMvcOptionMessageStartDivider =
        "==== Extra Mvc Option Action Info =====";

    /// <summary>
    /// JsonWebToken
    /// </summary>
    public const string JsonWebToken = "JsonWebToken";

    /// <summary>
    /// EasyToken
    /// </summary>
    public const string EasyToken = "EasyToken";

    /// <summary>
    /// SuperRoleName
    /// </summary>
    public const string SuperRoleName = "超级管理员";

    /// <summary>
    /// SuperRoleGuidTag
    /// </summary>
    public const string SuperRoleGuidTag = "super";

    /// <summary>
    /// UploadMaxSize
    /// </summary>
    public const decimal UploadMaxSize = 10;

    /// <summary>
    /// DefaultSupplierSettlementDelayDay
    /// </summary>
    public const int DefaultSupplierSettlementDelayDay = 7;

    /// <summary>
    /// DefaultAvatarImage
    /// </summary>
    public const string DefaultAvatarImage = "388a9b25-c3aa-440b-9dd6-5178066a3382.png";

    /// <summary>
    /// StringIdentificationValue
    /// </summary>
    public const string StringIdentificationValue = "-10000";

    /// <summary>
    /// ProductRankingWeightBeginTime
    /// </summary>
    public static readonly DateTime ProductRankingWeightBeginTime = new(2019, 1, 1, 0, 0, 0);

    /// <summary>
    /// ProductRankingWeightBeginUnixTime
    /// </summary>
    public const long ProductRankingWeightBeginUnixTime = 1546272000;

    /// <summary>
    /// UserCouponRankingWeightBeginTime
    /// </summary>
    public static readonly DateTime UserCouponRankingWeightBeginTime = new(2020, 1, 1, 0, 0, 0);

    /// <summary>
    /// UserCouponRankingWeightBeginUnixTime
    /// </summary>
    public const long UserCouponRankingWeightBeginUnixTime = 1577808000;

    /// <summary>
    /// IntIdentificationValue
    /// </summary>
    public const int IntIdentificationValue = -10000;

    /// <summary>
    /// LongIdentificationValue
    /// </summary>
    public const long LongIdentificationValue = -10000;

    /// <summary>
    /// IdentificationCodeDivider
    /// </summary>
    public const char IdentificationCodeDivider = '|';

    /// <summary>
    /// 匿名用户统一token
    /// </summary>
    public const string AnonymousToken = "anonymous";

    /// <summary>
    /// DateTimeDefault
    /// </summary>
    public static readonly DateTime DateTimeDefault = new(2010, 1, 1, 0, 0, 0);

    /// <summary>
    /// DateTimeDefaultString
    /// </summary>
    public const string DateTimeDefaultString = "2010-01-01 00:00:00";

    /// <summary>
    /// ApplicationStopNotificationText
    /// </summary>
    public static readonly string ApplicationStopNotificationText = "已经停止运行了哦";

    /// <summary>
    /// ApplicationPauseNotificationText
    /// </summary>
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