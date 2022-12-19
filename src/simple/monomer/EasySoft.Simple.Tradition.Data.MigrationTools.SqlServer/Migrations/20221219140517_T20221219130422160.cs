using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221219130422160 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<decimal>(
                name: "start_milliseconds",
                table: "sql_execution_record",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m,
                comment: "",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m,
                oldComment: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "first_fetch_duration_milliseconds",
                table: "sql_execution_record",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m,
                comment: "",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m,
                oldComment: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "duration_milliseconds",
                table: "sql_execution_record",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m,
                comment: "",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m,
                oldComment: "");

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 365746236174342L, "", "", "", 365746236174341L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 365746236174341L, 365746236170245L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 365746236170245L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365746236174343L, 365746236174342L, "c2f7fdac6482483781c71fad284811bf" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365746236174344L, 365746236174342L, "5e07ddac49aa4c7bacf0b32b5d3bd304" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365746236174345L, 365746236174342L, "348949be7da94cb7b02b56d53d303d59" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 365746236174341L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365746236174343L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365746236174344L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365746236174345L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 365746236170245L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 365746236174342L);

            migrationBuilder.AlterColumn<decimal>(
                name: "start_milliseconds",
                table: "sql_execution_record",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldDefaultValue: 0m,
                oldComment: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "first_fetch_duration_milliseconds",
                table: "sql_execution_record",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldDefaultValue: 0m,
                oldComment: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "duration_milliseconds",
                table: "sql_execution_record",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldDefaultValue: 0m,
                oldComment: "");

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
    }
}
