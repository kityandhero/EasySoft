namespace EasySoft.Simple.Tradition.Data.DataTransferObjects;

public class BlogDto
{
    public long BlogId { get; set; }

    public long CustomerId { get; set; }

    public string Title { get; set; }

    public BlogDto()
    {
        BlogId = 0;
        CustomerId = 0;
        Title = "";
    }
}