using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Pseudonym = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Motto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    real_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    login_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "id", "CustomerId", "Motto", "Name", "Pseudonym" },
                values: new object[] { 349398334455814L, 349398334455813L, "", "", "" });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "alias", "login_name", "password", "real_name" },
                values: new object[] { 349398334455813L, "粽子用户", "first", "123456", "张小明" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "id", "BlogId", "Title" },
                values: new object[] { 349398334455815L, 349398334455814L, "f11c83da344b4fcda64496a2a2f1cd6b" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "id", "BlogId", "Title" },
                values: new object[] { 349398334455816L, 349398334455814L, "4d9f974ae78b49178209d5c23ed4472d" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "id", "BlogId", "Title" },
                values: new object[] { 349398334455817L, 349398334455814L, "24d4de325cdc4f32bee9af2406a9c085" });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
