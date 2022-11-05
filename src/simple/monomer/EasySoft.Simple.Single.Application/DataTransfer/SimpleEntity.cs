namespace EasySoft.Simple.Single.Application.DataTransfer;

/// <summary>
/// 数据转换样例
/// </summary>
public class SimpleEntity
{
    public string Name { get; set; }

    public string Gender { get; set; }

    public int Age { get; set; }

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