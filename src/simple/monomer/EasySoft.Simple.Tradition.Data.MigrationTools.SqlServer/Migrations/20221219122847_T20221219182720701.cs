using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221219182720701 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 365375499022342L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365375499022344L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365375499022345L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365375499022346L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 365375499022341L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 365375499022343L);

            migrationBuilder.CreateTable(
                name: "general_log",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    message = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, defaultValue: "", comment: ""),
                    message_type = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: ""),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "", comment: ""),
                    content_type = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: ""),
                    type = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: ""),
                    channel = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: ""),
                    ip = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, defaultValue: "", comment: ""),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: ""),
                    create_by = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: ""),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: ""),
                    modify_by = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: ""),
                    modify_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_general_log", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 365722520887303L, "", "", "", 365722520887302L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 365722520887302L, 365722520887301L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 365722520887301L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365722520887304L, 365722520887303L, "30f71ba1770f4b489097d66399c94fd9" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365722520891397L, 365722520887303L, "51dab6b353ef47a2935373f10779efb3" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365722520891398L, 365722520887303L, "db530a4b706f45a5b6e03f7b2ea33cfb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "general_log");

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 365722520887302L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365722520887304L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365722520891397L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365722520891398L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 365722520887301L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 365722520887303L);

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 365375499022343L, "", "", "", 365375499022342L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 365375499022342L, 365375499022341L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 365375499022341L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365375499022344L, 365375499022343L, "89c72c3d81304d9fa306fcfb133b6766" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365375499022345L, 365375499022343L, "41be5cce6dfe458296b12dfbbbb281d9" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365375499022346L, 365375499022343L, "eca8e279f419432eb67a96c3d5347109" });
        }
    }
}
