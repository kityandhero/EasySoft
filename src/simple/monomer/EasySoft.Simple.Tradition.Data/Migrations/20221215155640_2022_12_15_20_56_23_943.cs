using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.Migrations
{
    public partial class _2022_12_15_20_56_23_943 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 364357936074757L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 364357936087045L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 364357936087046L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 364357936087047L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 364357936070661L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 364357936074758L);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { 364357936074758L, "", "", "", 364357936074757L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 364357936074757L, 364357936070661L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 364357936070661L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364357936087045L, 364357936074758L, "c8343da6cf164f5abf21fb548f174bcc" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364357936087046L, 364357936074758L, "d5dbd82a92a049d7aa87fa8e7c42b3ca" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364357936087047L, 364357936074758L, "c2d35d21609640af969a550e3ddf0514" });
        }
    }
}
