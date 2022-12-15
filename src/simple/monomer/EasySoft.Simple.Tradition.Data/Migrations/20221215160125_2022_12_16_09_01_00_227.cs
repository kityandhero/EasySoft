using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.Migrations
{
    public partial class _2022_12_16_09_01_00_227 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 364358033735685L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 364358033752069L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 364358033752070L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 364358033752071L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 364358033731589L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 364358033735686L);

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 364359199944710L, "", "", "", 364359199944709L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 364359199944709L, 364359199936517L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 364359199936517L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364359199969285L, 364359199944710L, "4ce4fb39ddd34a978443e2559d7289e5" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364359199969286L, 364359199944710L, "8a9399c1ddaf431fb1e96ea8257fddf3" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364359199969287L, 364359199944710L, "e03302c78bf740ddbf6f5ab26ecc57be" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 364359199944709L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 364359199969285L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 364359199969286L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 364359199969287L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 364359199936517L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 364359199944710L);

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 364358033735686L, "", "", "", 364358033735685L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 364358033735685L, 364358033731589L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 364358033731589L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364358033752069L, 364358033735686L, "ffee25700b824014a99adb447b023b53" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364358033752070L, 364358033735686L, "95ba156f9b0448bcb571c66322451d97" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364358033752071L, 364358033735686L, "ae94ed00e1734c488fe126b8563512a5" });
        }
    }
}
