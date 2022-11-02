namespace EasySoft.Simple.CustomerCenter.Application.Contracts.DataTransferObjects;

public class CustomerDto
{
    public int CustomerId { get; set; }

    public string Alias { get; set; }

    public CustomerDto()
    {
        CustomerId = 0;
        Alias = "";
    }
}