using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221224473318133 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 367439668916230L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367439668916232L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367439668916233L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367439668916234L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 367439668916229L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 367439668916231L);

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "app_security");

            migrationBuilder.DropColumn(
                name: "master_control",
                table: "app_security");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "deleted",
                table: "app_security",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "");

            migrationBuilder.AddColumn<int>(
                name: "master_control",
                table: "app_security",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "");

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 367439668916231L, "", "", "", 367439668916230L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 367439668916230L, 367439668916229L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 367439668916229L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367439668916232L, 367439668916231L, "bdc09767992445efbe993435228045f5" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367439668916233L, 367439668916231L, "1a9c3ef1deff4059b90074341ea01a58" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367439668916234L, 367439668916231L, "881bd928b7154d939bdda5c05d5e2617" });
        }
    }
}
