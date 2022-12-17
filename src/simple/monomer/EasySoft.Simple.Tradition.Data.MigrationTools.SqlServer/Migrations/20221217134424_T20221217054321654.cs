using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221217054321654 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 364701063847941L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 364701063847943L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 364701063847944L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 364701063847945L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 364701063843845L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 364701063847942L);

            migrationBuilder.CreateTable(
                name: "app_security",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    deleted = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: ""),
                    app_id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, defaultValue: "", comment: ""),
                    app_secret = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, defaultValue: "", comment: ""),
                    channel = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: ""),
                    ip = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, defaultValue: "", comment: ""),
                    create_by = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: ""),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: ""),
                    modify_by = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: ""),
                    modify_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_security", x => x.id);
                },
                comment: "应用安全");

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 365033315426311L, "", "", "", 365033315426310L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 365033315426310L, 365033315426309L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 365033315426309L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365033315426312L, 365033315426311L, "b50df5b3757a45f28aef5e98c4a719d9" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365033315426313L, 365033315426311L, "822d601e655c49aa824f6b2e74bda856" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365033315426314L, 365033315426311L, "1e2858c3c65e4cea90a3ad81eb8fc909" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_security");

            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 365033315426310L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365033315426312L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365033315426313L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365033315426314L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 365033315426309L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 365033315426311L);

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 364701063847942L, "", "", "", 364701063847941L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 364701063847941L, 364701063843845L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 364701063843845L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364701063847943L, 364701063847942L, "c621d0bbcfcb4ea7a2cda510ba345a24" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364701063847944L, 364701063847942L, "9276fde387014080a42de3f536c1f7ff" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364701063847945L, 364701063847942L, "578c3b887081402d9828e31026325552" });
        }
    }
}
