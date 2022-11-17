namespace EasySoft.Simple.Single.Application.DataTransfer;

/// <summary>
/// SimpleDto
/// </summary>
public class SimpleDto
{
    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// SimpleDto
    /// </summary>
    public SimpleDto()
    {
        Name = "";
        Address = "";
    }
}