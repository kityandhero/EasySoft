﻿// <auto-generated />
using EasySoft.Simple.Tradition.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.Migrations
{
    [DbContext(typeof(SqlServerDataContext))]
    [Migration("20221215155616_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EasySoft.Core.Infrastructure.Repositories.Entities.Implementations.EventTracker", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasComment("数据标识");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("EventId")
                        .HasColumnType("bigint")
                        .HasColumnName("event_id")
                        .HasComment("事件标识");

                    b.Property<string>("TrackerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tracker_name")
                        .HasComment("跟踪名称");

                    b.HasKey("Id")
                        .HasName("pk_event_tracker");

                    b.ToTable("event_tracker", (string)null);

                    b.HasComment("事件跟踪");
                });

            modelBuilder.Entity("EasySoft.Simple.Tradition.Data.Entities.Blog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Motto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("motto");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("Pseudonym")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("pseudonym");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_blog");

                    b.ToTable("blog", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 364357936074758L,
                            Motto = "",
                            Name = "",
                            Pseudonym = "",
                            UserId = 364357936074757L
                        });
                });

            modelBuilder.Entity("EasySoft.Simple.Tradition.Data.Entities.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_customer");

                    b.ToTable("customer", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 364357936074757L,
                            UserId = 364357936070661L
                        });
                });

            modelBuilder.Entity("EasySoft.Simple.Tradition.Data.Entities.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("BlogId")
                        .HasColumnType("bigint")
                        .HasColumnName("blog_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_post");

                    b.HasIndex("BlogId")
                        .HasDatabaseName("ix_post_blog_id");

                    b.ToTable("post", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 364357936087045L,
                            BlogId = 364357936074758L,
                            Title = "c8343da6cf164f5abf21fb548f174bcc"
                        },
                        new
                        {
                            Id = 364357936087046L,
                            BlogId = 364357936074758L,
                            Title = "d5dbd82a92a049d7aa87fa8e7c42b3ca"
                        },
                        new
                        {
                            Id = 364357936087047L,
                            BlogId = 364357936074758L,
                            Title = "c2d35d21609640af969a550e3ddf0514"
                        });
                });

            modelBuilder.Entity("EasySoft.Simple.Tradition.Data.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("alias");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("login_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("RealName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("real_name");

                    b.Property<long>("RoleGroupId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_group_id");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.ToTable("user", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 364357936070661L,
                            Alias = "种子用户",
                            LoginName = "first",
                            Password = "e10adc3949ba59abbe56e057f20f883e",
                            RealName = "张小明",
                            RoleGroupId = 1L
                        });
                });

            modelBuilder.Entity("EasySoft.Simple.Tradition.Data.Entities.Post", b =>
                {
                    b.HasOne("EasySoft.Simple.Tradition.Data.Entities.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_post_blog_blog_id");

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("EasySoft.Simple.Tradition.Data.Entities.Blog", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
