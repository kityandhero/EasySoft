using EasySoft.Simple.Tradition.Data.Entities.Bases;

namespace EasySoft.Simple.Tradition.Data.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; }

    public long BlogId { get; set; }

    /// <summary>
    /// 博客标识
    /// </summary>
    public Blog? Blog { get; set; }

    public Post()
    {
        Title = "";
    }
}