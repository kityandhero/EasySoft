namespace EasySoft.Core.PermissionVerification.Entities;

public class AccessWayModel
{
    [Description("名称")]
    public string Name { get; set; }

    [Description("识别标识")]
    public string GuidTag { get; set; }

    [Description("相对路径")]
    public string RelativePath { get; set; }

    [Description("扩展权限")]
    public string Expand { get; set; }

    [Description("渠道码")]
    public int Channel { get; set; }

    public AccessWayModel()
    {
        Name = "";
        GuidTag = "";
        RelativePath = "";
        Expand = "";
        Channel = 0;
    }
}