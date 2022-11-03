using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blog",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pseudonym = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    motto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog", x => x.id);
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
                        name: "FK_Posts_blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "blog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "CustomerId", "motto", "pseudonym", "title" },
                values: new object[] { 349393606803462L, 349393606803461L, "", "", "" });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "alias", "login_name", "password", "real_name" },
                values: new object[] { 349393606803461L, "粽子用户", "first", "123456", "张小明" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "id", "BlogId", "Title" },
                values: new object[] { 349393606803463L, 349393606803462L, "766d09cd4a0046d78bf57e8b20093d1c" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "id", "BlogId", "Title" },
                values: new object[] { 349393606803464L, 349393606803462L, "8f3c1268b3674c43adcce219ea579828" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "id", "BlogId", "Title" },
                values: new object[] { 349393606803465L, 349393606803462L, "b018925aa4da4ed1aa06562a3bc15f58" });

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
                name: "blog");
        }
    }
}
