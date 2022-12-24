using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221224183116199 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "master_control",
                table: "app_security",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "");

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 367433846579207L, "", "", "", 367433846579206L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 367433846579206L, 367433846579205L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 367433846579205L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367433846579208L, 367433846579207L, "2cfd2f524bca441197ed6974f348b3ec" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367433846579209L, 367433846579207L, "ae1a3f6f24cd4208bce8814405c7b6b9" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367433846579210L, 367433846579207L, "a50cd3b060bf4e909387b469aa356b36" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 367433846579206L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367433846579208L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367433846579209L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367433846579210L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 367433846579205L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 367433846579207L);

            migrationBuilder.AlterColumn<int>(
                name: "master_control",
                table: "app_security",
                type: "int",
                nullable: false,
                comment: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0,
                oldComment: "");

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
    }
}
