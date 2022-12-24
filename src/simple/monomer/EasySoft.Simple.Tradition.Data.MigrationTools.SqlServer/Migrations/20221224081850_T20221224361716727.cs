using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221224361716727 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 366432981438469L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 366432981438471L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 366432981438472L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 366432981438473L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 366432981434373L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 366432981438470L);

            migrationBuilder.AddColumn<int>(
                name: "channel",
                table: "preset_role",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "");

            migrationBuilder.AddColumn<int>(
                name: "channel",
                table: "custom_role",
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
                values: new object[] { 367430566260743L, "", "", "", 367430566260742L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 367430566260742L, 367430566260741L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 367430566260741L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367430566264837L, 367430566260743L, "08ff52cb2c014aafab0ae7ee37ca0252" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367430566264838L, 367430566260743L, "d0f4fe1d31cf455ab136a61c8dadae4a" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367430566264839L, 367430566260743L, "333c389b1e6047adb00281201aa15783" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 367430566260742L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367430566264837L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367430566264838L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367430566264839L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 367430566260741L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 367430566260743L);

            migrationBuilder.DropColumn(
                name: "channel",
                table: "preset_role");

            migrationBuilder.DropColumn(
                name: "channel",
                table: "custom_role");

            migrationBuilder.DropColumn(
                name: "master_control",
                table: "app_security");

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 366432981438470L, "", "", "", 366432981438469L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 366432981438469L, 366432981434373L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 366432981434373L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 366432981438471L, 366432981438470L, "176ef8efac434a33af551b2a4ff8c9f9" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 366432981438472L, 366432981438470L, "02e62e5844ac40c397600c42bb9c13e7" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 366432981438473L, 366432981438470L, "10c7b45680aa4e90adda17f03768669f" });
        }
    }
}
