using EasySoft.Core.EntityFramework.EntityFactories;
using EasySoft.IdGenerator.Assists;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.UtilityTools.Standard.Assists;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Simple.Tradition.Data.ExtensionMethods;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var customer = EntityFactory.Create<Customer>();

        customer.Alias = "种子用户";
        customer.RealName = "张小明";
        customer.LoginName = "first";
        customer.Password = "123456";

        modelBuilder.Entity<Customer>().HasData(
            customer
        );

        var blog = EntityFactory.Create<Blog>();

        blog.CustomerId = customer.Id;

        modelBuilder.Entity<Blog>().HasData(
            blog
        );

        modelBuilder.Entity<Post>().HasData(
            new List<Post>
            {
                new()
                {
                    Id = IdentifierAssist.Create(),
                    Title = UniqueIdAssist.CreateUUID(),
                    BlogId = blog.Id
                },
                new()
                {
                    Id = IdentifierAssist.Create(),
                    Title = UniqueIdAssist.CreateUUID(),
                    BlogId = blog.Id
                },
                new()
                {
                    Id = IdentifierAssist.Create(),
                    Title = UniqueIdAssist.CreateUUID(),
                    BlogId = blog.Id
                }
            }
        );
    }
}