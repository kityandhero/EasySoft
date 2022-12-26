using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221226090510345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 367464070381574L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367464070381576L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367464070385669L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367464070385670L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 367464070381573L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 367464070381575L);

            migrationBuilder.AddColumn<int>(
                name: "whether_super",
                table: "role_group",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "超级管理角色组");

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 368046988091398L, "", "", "", 368046988091397L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 368046988091397L, 368046988087301L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 368046988087301L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 368046988091399L, 368046988091398L, "f7937d41912c4b39bc83e8dcf92aa011" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 368046988091400L, 368046988091398L, "a7b1d72992e54712b7ad74ce7b31814d" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 368046988091401L, 368046988091398L, "a44d85ab70104f8fac28b8ba35a1659b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 368046988091397L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 368046988091399L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 368046988091400L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 368046988091401L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 368046988087301L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 368046988091398L);

            migrationBuilder.DropColumn(
                name: "whether_super",
                table: "role_group");

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 367464070381575L, "", "", "", 367464070381574L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 367464070381574L, 367464070381573L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 367464070381573L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367464070381576L, 367464070381575L, "7a8ee8f943c44b7b8b57eda54f575847" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367464070385669L, 367464070381575L, "6ac93eb7011241b7befdaf4e93c1f8bc" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367464070385670L, 367464070381575L, "78544ffd028f47878a21110fe8ca0d5d" });
        }
    }
}
