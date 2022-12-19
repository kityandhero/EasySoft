using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221219345321235 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "sql_execution_record",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    command_string = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "", comment: ""),
                    execute_type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: ""),
                    stack_trace_snippet = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "", comment: ""),
                    start_milliseconds = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m, comment: ""),
                    duration_milliseconds = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m, comment: ""),
                    first_fetch_duration_milliseconds = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m, comment: ""),
                    errored = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: ""),
                    collect_mode = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: ""),
                    database_channel = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, defaultValue: "", comment: ""),
                    channel = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: ""),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: ""),
                    ip = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: ""),
                    create_by = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: ""),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: ""),
                    modify_by = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: ""),
                    modify_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sql_execution_record", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 365743699324935L, "", "", "", 365743699324934L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 365743699324934L, 365743699324933L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 365743699324933L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365743699324936L, 365743699324935L, "47f9edc8646343c1a9952ff197884a58" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365743699324937L, 365743699324935L, "e46546e750564dfba1e354d03b36dfda" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365743699324938L, 365743699324935L, "7dc85d1a3b60432ca2366126c24ec64d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sql_execution_record");

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 365743699324934L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365743699324936L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365743699324937L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365743699324938L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 365743699324933L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 365743699324935L);

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
    }
}
