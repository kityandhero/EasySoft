namespace EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;

/// <summary>
/// UserDto
/// </summary>
public class UserDto
{
    /// <summary>
    /// UserId
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// UserDto
    /// </summary>
    public UserDto()
    {
        UserId = 0;
        Alias = "";
    }
}