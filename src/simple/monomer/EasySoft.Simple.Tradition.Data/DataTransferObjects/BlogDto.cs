namespace EasySoft.Simple.Tradition.Data.DataTransferObjects;

public class BlogDto
{
    public long CustomerId { get; set; }

    public string Title { get; set; }

    public BlogDto()
    {
        CustomerId = 0;
        Title = "";
    }
}