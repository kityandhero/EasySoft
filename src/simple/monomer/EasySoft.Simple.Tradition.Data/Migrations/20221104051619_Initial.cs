using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blog",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "主键标识"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: "博客名称"),
                    pseudonym = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: ""),
                    motto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: ""),
                    customer_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog", x => x.id);
                },
                comment: "blog");

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "主键标识"),
                    alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "昵称"),
                    real_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "真实姓名"),
                    login_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "登录名"),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "", comment: "密码")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.id);
                },
                comment: "customer");

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "主键标识"),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: ""),
                    BlogId = table.Column<long>(type: "bigint", nullable: false),
                    BlogId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.id);
                    table.ForeignKey(
                        name: "FK_post_blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "blog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_blog_BlogId1",
                        column: x => x.BlogId1,
                        principalTable: "blog",
                        principalColumn: "id");
                },
                comment: "post");

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "customer_id", "motto", "name", "pseudonym" },
                values: new object[] { 349690990989318L, 349690990989317L, "", "", "" });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "alias", "login_name", "password", "real_name" },
                values: new object[] { 349690990989317L, "粽子用户", "first", "123456", "张小明" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "BlogId", "BlogId1", "title" },
                values: new object[] { 349690990989319L, 349690990989318L, null, "bb57b6d2ce714de1875a19b40aaff80a" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "BlogId", "BlogId1", "title" },
                values: new object[] { 349690990989320L, 349690990989318L, null, "db42d66b3c804206b23cf7230cf7aef3" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "BlogId", "BlogId1", "title" },
                values: new object[] { 349690990989321L, 349690990989318L, null, "8763abcd34b34bf0b898ea2b074a68cd" });

            migrationBuilder.CreateIndex(
                name: "IX_post_BlogId",
                table: "post",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_post_BlogId1",
                table: "post",
                column: "BlogId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "blog");
        }
    }
}
