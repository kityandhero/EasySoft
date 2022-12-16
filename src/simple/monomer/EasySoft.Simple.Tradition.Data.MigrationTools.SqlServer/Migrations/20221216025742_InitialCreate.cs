using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "access_way",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, defaultValue: "", comment: "名称"),
                    guid_tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "识别标识"),
                    relative_path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValue: "", comment: "相对路径"),
                    expand = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, defaultValue: "", comment: "扩展权限"),
                    channel = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "渠道码"),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "状态码"),
                    ip = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, defaultValue: "", comment: "Ip"),
                    create_user_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "创建人标识"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "创建时间"),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "更新人标识"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "更新时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_way", x => x.id);
                },
                comment: "访问模块");

            migrationBuilder.CreateTable(
                name: "blog",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: "名称"),
                    pseudonym = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: "笔名"),
                    motto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: "座右铭"),
                    user_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "用户标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blog", x => x.id);
                },
                comment: "博客");

            migrationBuilder.CreateTable(
                name: "custom_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, defaultValue: "", comment: "名称"),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValue: "", comment: "简介描述"),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "", comment: "内容详情"),
                    module_count = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "模块数量"),
                    competence = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "", comment: "权限集合"),
                    whether_super = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "超级管理"),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "状态码"),
                    ip = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, defaultValue: "", comment: "Ip"),
                    create_user_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "创建人标识"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "创建时间"),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "更新人标识"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "更新时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_custom_role", x => x.id);
                },
                comment: "自定义角色");

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    user_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "用户标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                },
                comment: "顾客信息");

            migrationBuilder.CreateTable(
                name: "event_tracker",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    event_id = table.Column<long>(type: "bigint", nullable: false, comment: "事件标识"),
                    tracker_name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "跟踪名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_tracker", x => x.id);
                },
                comment: "事件跟踪");

            migrationBuilder.CreateTable(
                name: "preset_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, defaultValue: "", comment: "名称"),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValue: "", comment: "简介描述"),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "", comment: "内容详情"),
                    module_count = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "模块数量"),
                    competence = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "", comment: "权限集合"),
                    whether_super = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "超级管理"),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "状态码"),
                    ip = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, defaultValue: "", comment: "Ip"),
                    create_user_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "创建人标识"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "创建时间"),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "更新人标识"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "更新时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preset_role", x => x.id);
                },
                comment: "预设角色");

            migrationBuilder.CreateTable(
                name: "role_group",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    name = table.Column<string>(type: "nvarchar", nullable: false, defaultValue: "", comment: "角色组名"),
                    custom_role_collection = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "", comment: "自定义角色集合"),
                    preset_role_collection = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "", comment: "预设角色集合"),
                    channel = table.Column<int>(type: "int", nullable: false, comment: "渠道码"),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "状态码"),
                    ip = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, defaultValue: "", comment: "Ip"),
                    create_user_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "创建人标识"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "创建时间"),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L, comment: "更新人标识"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), comment: "更新时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_group", x => x.id);
                },
                comment: "角色组");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "别名"),
                    real_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "真实姓名"),
                    login_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "", comment: "登录名"),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "", comment: "密码"),
                    role_group_id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                },
                comment: "基础账户");

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "数据标识"),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "", comment: ""),
                    blog_id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_blog_blog_id",
                        column: x => x.blog_id,
                        principalTable: "blog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "blog",
                columns: new[] { "id", "motto", "name", "pseudonym", "user_id" },
                values: new object[] { 364520490926087L, "", "", "", 364520490926086L });

            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "id", "user_id" },
                values: new object[] { 364520490926086L, 364520490926085L });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "alias", "login_name", "password", "real_name", "role_group_id" },
                values: new object[] { 364520490926085L, "种子用户", "first", "e10adc3949ba59abbe56e057f20f883e", "张小明", 1L });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364520490926088L, 364520490926087L, "3e85e1a0a6ee4ba1804010557e816bc2" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364520490930181L, 364520490926087L, "d5dcf5e5525d4021a740a2271dcf0999" });

            migrationBuilder.InsertData(
                table: "post",
                columns: new[] { "id", "blog_id", "title" },
                values: new object[] { 364520490930182L, 364520490926087L, "2ba0a75f6d0b45cd8c8ad9bd927aa700" });

            migrationBuilder.CreateIndex(
                name: "ix_post_blog_id",
                table: "post",
                column: "blog_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_way");

            migrationBuilder.DropTable(
                name: "custom_role");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "event_tracker");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "preset_role");

            migrationBuilder.DropTable(
                name: "role_group");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "blog");
        }
    }
}
