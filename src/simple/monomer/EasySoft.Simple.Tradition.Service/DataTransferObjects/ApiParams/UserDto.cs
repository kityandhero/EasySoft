namespace EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;

public class UserDto
{
    public int UserId { get; set; }

    public string Alias { get; set; }

    public UserDto()
    {
        UserId = 0;
        Alias = "";
    }
}