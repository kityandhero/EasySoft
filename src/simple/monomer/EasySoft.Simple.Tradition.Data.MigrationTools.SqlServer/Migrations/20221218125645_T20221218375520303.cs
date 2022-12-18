using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221218375520303 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "app_public_key",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: ""),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: ""),
                    modify_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_public_key", x => x.id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_public_key");

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
    }
}
