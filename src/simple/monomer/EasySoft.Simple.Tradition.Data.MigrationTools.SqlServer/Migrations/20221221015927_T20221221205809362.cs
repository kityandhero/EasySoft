using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class T20221221205809362 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
