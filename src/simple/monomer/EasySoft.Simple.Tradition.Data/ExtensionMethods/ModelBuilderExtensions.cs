using EasySoft.IdGenerator.Assists;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.UtilityTools.Standard.Assists;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.Tradition.Data.ExtensionMethods;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var customer = new Customer
        {
            Id = IdentifierAssist.Create(),
            Alias = "粽子用户",
            RealName = "张小明",
            LoginName = "first",
            Password = "123456"
        };

        modelBuilder.Entity<Customer>().HasData(
            customer
        );

        var author = new Author
        {
            Id = IdentifierAssist.Create(),
            CustomerId = customer.Id
        };

        var blog = new Blog
        {
            Id = IdentifierAssist.Create(),
            AuthorId = author.Id
        };

        author.Blog = blog;

        var posts = new List<Post>
        {
            new()
            {
                Title = UniqueIdAssist.CreateUUID(),
                Author = author,
                Blog = blog
            },
            new()
            {
                Title = UniqueIdAssist.CreateUUID(),
                Author = author,
                Blog = blog
            },
            new()
            {
                Title = UniqueIdAssist.CreateUUID(),
                Author = author,
                Blog = blog
            }
        };

        modelBuilder.Entity<Author>().HasData(
            author
        );

        modelBuilder.Entity<Blog>().HasData(
            blog
        );

        foreach (var post in posts)
            modelBuilder.Entity<Post>().HasData(
                post
            );
    }
}