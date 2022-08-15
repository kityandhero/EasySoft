using EntityFrameworkTest.Bases;

namespace EntityFrameworkTest.Entities;

public class Author : BaseEntity
{
    public int AuthorId { get; set; }

    public string AuthorName { get; set; }

    public Author()
    {
        AuthorName = "";
    }
}