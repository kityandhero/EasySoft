using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221224515416756 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "deleted",
                table: "app_security",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0,
                oldComment: "")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 367439668916231L, "", "", "", 367439668916230L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 367439668916230L, 367439668916229L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 367439668916229L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367439668916232L, 367439668916231L, "bdc09767992445efbe993435228045f5" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367439668916233L, 367439668916231L, "1a9c3ef1deff4059b90074341ea01a58" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 367439668916234L, 367439668916231L, "881bd928b7154d939bdda5c05d5e2617" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "id",
                keyValue: 367439668916230L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367439668916232L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367439668916233L);

            migrationBuilder.DeleteData(
                table: "post",
                keyColumn: "id",
                keyValue: 367439668916234L);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 367439668916229L);

            migrationBuilder.DeleteData(
                table: "blog",
                keyColumn: "id",
                keyValue: 367439668916231L);

            migrationBuilder.AlterColumn<int>(
                name: "deleted",
                table: "app_security",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0,
                oldComment: "")
                .Annotation("Relational:ColumnOrder", 2);

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
    }
}
