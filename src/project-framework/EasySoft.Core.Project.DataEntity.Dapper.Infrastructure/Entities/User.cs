namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("user")]
public class User : AbstractFunctionEntity<User>, IUserPure
{
    #region Properties

    [AdvanceColumnMapper("user_group_id")]
    public long UserGroupId { get; set; }

    [AdvanceColumnMapper("parent_id")]
    public long ParentId { get; set; }

    [AdvanceColumnInformation("昵称")]
    [AdvanceColumnMapper("nickname")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Nickname { get; set; }

    [AdvanceColumnInformation("真实姓名")]
    [AdvanceColumnMapper("real_name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string RealName { get; set; }

    [AdvanceColumnInformation("性别")]
    [AdvanceColumnMapper("gender")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public int Gender { get; set; }

    [AdvanceColumnInformation("身份证号")]
    [AdvanceColumnMapper("identity_number")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public string IdentityNumber { get; set; }

    [AdvanceColumnInformation("手机号码")]
    [AdvanceColumnMapper("phone")]
    [AdvanceColumnLength(50)]
    public string Phone { get; set; }

    [AdvanceColumnInformation("电子邮件")]
    [AdvanceColumnMapper("email")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Email { get; set; }

    [AdvanceColumnInformation("头像")]
    [AdvanceColumnMapper("avatar")]
    [AdvanceColumnLength(400)]
    public string Avatar { get; set; }

    [AdvanceColumnInformation("国家")]
    [AdvanceColumnMapper("country")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public string CountryName { get; set; }

    [AdvanceColumnInformation("省份代码")]
    [AdvanceColumnMapper("province_code")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public long ProvinceCode { get; set; }

    [AdvanceColumnInformation("省份")]
    [AdvanceColumnMapper("province_name")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public string ProvinceName { get; set; }

    [AdvanceColumnInformation("城市代码")]
    [AdvanceColumnMapper("city_code")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public long CityCode { get; set; }

    [AdvanceColumnInformation("城市")]
    [AdvanceColumnMapper("city_name")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public string CityName { get; set; }

    [AdvanceColumnInformation("区县代码")]
    [AdvanceColumnMapper("district_code")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public long DistrictCode { get; set; }

    [AdvanceColumnInformation("区县")]
    [AdvanceColumnMapper("district_name")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public string DistrictName { get; set; }

    [AdvanceColumnInformation("详细地址")]
    [AdvanceColumnMapper("address")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public string Address { get; set; }

    [AdvanceColumnInformation("用户类型")]
    [AdvanceColumnMapper("type")]
    public int Type { get; set; }

    [AdvanceColumnInformation("生日")]
    [AdvanceColumnMapper("birthday")]
    [AdvanceColumnDefaultValue(ConstCollection.DatabaseDefaultDateTime)]
    public DateTime Birthday { get; set; }

    [AdvanceColumnInformation("印章")]
    [AdvanceColumnMapper("signet")]
    [AdvanceColumnLength(400)]
    public string Signet { get; set; }

    [AdvanceColumnInformation("印章密码开关")]
    [AdvanceColumnMapper("signet_password_switch")]
    [AdvanceColumnLength(100)]
    public int SignetPasswordSwitch { get; set; }

    [AdvanceColumnInformation("印章密码")]
    [AdvanceColumnMapper("signet_password")]
    [AdvanceColumnLength(100)]
    public string SignetPassword { get; set; }

    [AdvanceColumnInformation("维度")]
    [AdvanceColumnMapper("latitude")]
    public decimal Latitude { get; set; }

    [AdvanceColumnInformation("经度")]
    [AdvanceColumnMapper("longitude")]
    public decimal Longitude { get; set; }

    [AdvanceColumnInformation("位置编码")]
    [AdvanceColumnMapper("geo_hash_long")]
    public long GeoHashLong { get; set; }

    [AdvanceColumnInformation("位置编码")]
    [AdvanceColumnMapper("geo_hash")]
    [AdvanceColumnLength(100)]
    public string GeoHash { get; set; }

    #endregion Properties

    #region Constructor

    public User()
    {
        UserGroupId = 0;
        ParentId = 0;
        Nickname = "";
        RealName = "";
        Gender = UtilityTools.Standard.Enums.Gender.Unknown.ToInt();
        IdentityNumber = "";
        CountryName = "";
        ProvinceCode = 0;
        ProvinceName = "";
        CityCode = 0;
        CityName = "";
        DistrictCode = 0;
        DistrictName = "";
        Address = "";
        Avatar = "";
        Phone = "";
        Type = 0;
        Email = "";
        Avatar = "";
        Signet = "";
        SignetPasswordSwitch = 0;
        SignetPassword = "";
        Latitude = 0;
        Longitude = 0;
        GeoHash = "";
        GeoHashLong = 0;
    }

    #endregion Constructor
}