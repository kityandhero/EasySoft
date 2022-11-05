namespace EasySoft.Simple.Single.Application.DataTransfer;

/// <summary>
/// 数据转换样例
/// </summary>
public class SimpleEntity
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gender
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// Age
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// SimpleEntity
    /// </summary>
    public SimpleEntity()
    {
        Name = "";
        Gender = "";
        Age = 0;
        Address = "";
    }
}