using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221221023820681 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 366423425122309L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 366423425122311L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 366423425122312L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 366423425122313L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 366423425118213L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 366423425122310L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "super_role_recently_maintain_time",
                table: "app_security",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "super_role_next_maintain_time",
                table: "app_security",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "super_role_recently_maintain_time",
                table: "app_security",
                type: "datetime2",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldComment: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "super_role_next_maintain_time",
                table: "app_security",
                type: "datetime2",
                nullable: false,
                comment: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldComment: "");

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 366423425122310L, "", "", "", 366423425122309L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 366423425122309L, 366423425118213L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 366423425118213L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 366423425122311L, 366423425122310L, "dee98242a84b44dd9dd7fadb0a50a280" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 366423425122312L, 366423425122310L, "7b3ca34632714566bbd7f0878a88167c" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 366423425122313L, 366423425122310L, "fb5e30f6f8d142a8aa9cbaa710b759c6" });
        }
    }
}
