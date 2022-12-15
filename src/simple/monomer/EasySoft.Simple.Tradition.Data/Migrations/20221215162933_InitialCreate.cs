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
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: "名称"),
                    pseudonym = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: "笔名"),
                    motto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: "座右铭"),
                    user_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "用户标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blog", x => x.id);
                },
                comment: "博客");

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    user_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "用户标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                },
                comment: "顾客信息");

            migrationBuilder.CreateTable(
                name: "event_tracker",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    event_id = table.Column<long>(type: "bigint", nullable: false, comment: "事件标识"),
                    tracker_name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "跟踪名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_tracker", x => x.id);
                },
                comment: "事件跟踪");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "别名"),
                    real_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "真实姓名"),
                    login_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "登录名"),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "", comment: "密码"),
                    role_group_id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                },
                comment: "基础账户");

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: ""),
                    blog_id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_blog_blog_id",
                        column: x => x.blog_id,
                        principalTable: "blog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 364366116073479L, "", "", "", 364366116073478L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 364366116073478L, 364366116073477L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 364366116073477L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364366116073480L, 364366116073479L, "5cb3e93dd7b84a2bb238bdd80fafa31b" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364366116073481L, 364366116073479L, "c039271ed1a14a51ac26da13f8c900c3" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364366116073482L, 364366116073479L, "11141e9cb3b9425ca3499cc112583e6e" });

            migrationBuilder.CreateIndex(
                name: "ix_post_blog_id",
                table: "post",
                column: "blog_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "event_tracker");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "blog");
        }
    }
}
