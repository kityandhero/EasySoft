using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkTest.Migrations
{
    public partial class Addrs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "Blogs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "BlogName",
                table: "Blogs",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Authors",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "AuthorName",
                table: "Authors",
                newName: "real_name");

            migrationBuilder.AddColumn<string>(
                name: "login_name",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "login_name",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Blogs",
                newName: "BlogId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Blogs",
                newName: "BlogName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Authors",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "real_name",
                table: "Authors",
                newName: "AuthorName");
        }
    }
}
