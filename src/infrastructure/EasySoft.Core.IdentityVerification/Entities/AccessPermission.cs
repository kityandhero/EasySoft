namespace EasySoft.Core.IdentityVerification.Entities;

public class AccessPermission
{
    public string Name { get; set; }

    public string Competence { get; set; }

    public string GuidTag { get; set; }

    public string Url { get; set; }

    public string Path { get; set; }

    public AccessPermission()
    {
        Name = "";
        Competence = "";
        GuidTag = "";
        Url = "";
        Path = "";
    }
}