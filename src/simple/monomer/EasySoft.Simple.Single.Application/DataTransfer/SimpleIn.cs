namespace EasySoft.Simple.Single.Application.DataTransfer;

/// <summary>
/// SimpleIn
/// </summary>
public class SimpleIn
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gender
    /// </summary>
    public string Gender { get; set; }

    public int Age { get; set; }

    /// <summary>
    /// SimpleIn
    /// </summary>
    public SimpleIn()
    {
        Name = "";
        Gender = "";
        Age = 0;
    }
}