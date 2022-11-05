namespace EasySoft.Simple.DomainDrivenDesign.Application.Contracts.DataTransferObjects;

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