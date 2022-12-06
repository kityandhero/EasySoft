using EasySoft.Simple.Tradition.Data.Entities;

namespace EasySoft.Simple.Tradition.Data.ExtensionMethods;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var roleGroup = EntityFactory.Create<RoleGroup>();

        roleGroup.Id = 1;
        roleGroup.Name = "空白用户组";
        roleGroup.Channel = 0;

        modelBuilder.Entity<RoleGroup>().HasData(
            roleGroup
        );

        var user = EntityFactory.Create<User>();

        user.Alias = "种子用户";
        user.RealName = "张小明";
        user.LoginName = "first";
        user.Password = "123456";
        user.RoleGroupId = 1;

        modelBuilder.Entity<User>().HasData(
            user
        );

        var customer = EntityFactory.Create<Customer>();

        customer.UserId = user.Id;

        modelBuilder.Entity<Customer>().HasData(
            customer
        );

        var blog = EntityFactory.Create<Blog>();

        blog.UserId = customer.Id;

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