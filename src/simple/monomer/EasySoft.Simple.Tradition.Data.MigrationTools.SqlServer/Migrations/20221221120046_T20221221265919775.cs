using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221221265919775 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 366275645825029L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 366275645825031L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 366275645825032L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 366275645825033L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 366275645820933L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 366275645825030L);

            migrationBuilder.AddColumn<DateTime>(
                name: "super_role_next_maintain_time",
                table: "app_security",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "super_role_recently_maintain_time",
                table: "app_security",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "super_role_next_maintain_time",
                table: "app_security");

            migrationBuilder.DropColumn(
                name: "super_role_recently_maintain_time",
                table: "app_security");

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 366275645825030L, "", "", "", 366275645825029L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 366275645825029L, 366275645820933L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 366275645820933L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 366275645825031L, 366275645825030L, "dcc3e4604be94d7881b0f63df2ebe430" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 366275645825032L, 366275645825030L, "c102034a054b4fdfa97617f7914f11d0" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 366275645825033L, 366275645825030L, "ad43b910bba34cbf9b7c665d3a9c2a3d" });
        }
    }
}
