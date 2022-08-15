using EntityFrameworkTest.Bases;

namespace EntityFrameworkTest.Entities;

public class Blog : BaseEntity
{
    public int BlogId { get; set; }

    public string BlogName { get; set; }

    public int AuthorId { get; set; }

    public Blog()
    {
        BlogName = "";
    }
}