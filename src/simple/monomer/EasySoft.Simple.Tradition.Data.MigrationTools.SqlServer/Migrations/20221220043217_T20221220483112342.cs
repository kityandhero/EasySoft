using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221220483112342 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "execute_type",
                table: "sql_execution_record",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "0",
                comment: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldDefaultValue: "",
                oldComment: "");

            migrationBuilder.AddColumn<string>(
                name: "execute_guid",
                table: "sql_execution_record",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "");

            migrationBuilder.AddColumn<string>(
                name: "execute_type_source",
                table: "sql_execution_record",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "");

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 365959310934022L, "", "", "", 365959310934021L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 365959310934021L, 365959310929925L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 365959310929925L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365959310934023L, 365959310934022L, "f862a33547724bcd80adff639f2b1890" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365959310934024L, 365959310934022L, "cd44ff2ad4454da8a098f79ef3a1e32f" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 365959310934025L, 365959310934022L, "e464ba4d761a4e1c826f22a0c1921eb4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 365959310934021L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365959310934023L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365959310934024L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 365959310934025L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 365959310929925L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 365959310934022L);

            migrationBuilder.DropColumn(
                name: "execute_guid",
                table: "sql_execution_record");

            migrationBuilder.DropColumn(
                name: "execute_type_source",
                table: "sql_execution_record");

            migrationBuilder.AlterColumn<string>(
                name: "execute_type",
                table: "sql_execution_record",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldDefaultValue: "0",
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
    }
}
