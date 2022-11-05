namespace EasySoft.Simple.Tradition.Data.DataTransferObjects;

public class BlogDto
{
    public long BlogId { get; set; }

    public long UserId { get; set; }

    public string Title { get; set; }

    public BlogDto()
    {
        BlogId = 0;
        UserId = 0;
        Title = "";
    }
}