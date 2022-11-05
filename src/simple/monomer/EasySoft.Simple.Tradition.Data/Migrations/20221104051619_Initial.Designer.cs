﻿// <auto-generated />
using System;
using EasySoft.Simple.Tradition.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EasySoft.Simple.Tradition.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221104051619_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EasySoft.Simple.Tradition.Data.Entities.Blog", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L)
                        .HasColumnName("id")
                        .HasColumnOrder(1);

                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L)
                        .HasColumnName("customer_id");

                    b.Property<string>("Motto")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValue("")
                        .HasColumnName("motto");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValue("")
                        .HasColumnName("name");

                    b.Property<string>("Pseudonym")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValue("")
                        .HasColumnName("pseudonym");

                    b.HasKey("Id");

                    b.ToTable("blog", (string)null);

                    b.HasComment("blog");

                    b.HasData(
                        new
                        {
                            Id = 349690990989318L,
                            CustomerId = 349690990989317L,
                            Motto = "",
                            Name = "",
                            Pseudonym = ""
                        });
                });

            modelBuilder.Entity("EasySoft.Simple.Tradition.Data.Entities.Customer", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L)
                        .HasColumnName("id")
                        .HasColumnOrder(1)
                        .HasComment("主键标识");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("")
                        .HasColumnName("alias")
                        .HasComment("昵称");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("")
                        .HasColumnName("login_name")
                        .HasComment("登录名");

                    b.Property<string>("Password")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("")
                        .HasColumnName("password")
                        .HasComment("密码");

                    b.Property<string>("RealName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("")
                        .HasColumnName("real_name")
                        .HasComment("真实姓名");

                    b.HasKey("Id");

                    b.ToTable("customer", (string)null);

                    b.HasComment("customer");

                    b.HasData(
                        new
                        {
                            Id = 349690990989317L,
                            Alias = "粽子用户",
                            LoginName = "first",
                            Password = "123456",
                            RealName = "张小明"
                        });
                });

            modelBuilder.Entity("EasySoft.Simple.Tradition.Data.Entities.Post", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L)
                        .HasColumnName("id")
                        .HasColumnOrder(1)
                        .HasComment("主键标识");

                    b.Property<long>("BlogId")
                        .HasColumnType("bigint");

                    b.Property<long?>("BlogId1")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValue("")
                        .HasColumnName("title")
                        .HasComment("");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("BlogId1");

                    b.ToTable("post", (string)null);

                    b.HasComment("post");

                    b.HasData(
                        new
                        {
                            Id = 349690990989319L,
                            BlogId = 349690990989318L,
                            Title = "bb57b6d2ce714de1875a19b40aaff80a"
                        },
                        new
                        {
                            Id = 349690990989320L,
                            BlogId = 349690990989318L,
                            Title = "db42d66b3c804206b23cf7230cf7aef3"
                        },
                        new
                        {
                            Id = 349690990989321L,
                            BlogId = 349690990989318L,
                            Title = "8763abcd34b34bf0b898ea2b074a68cd"
                        });
                });

            modelBuilder.Entity("EasySoft.Simple.Tradition.Data.Entities.Post", b =>
                {
                    b.HasOne("EasySoft.Simple.Tradition.Data.Entities.Blog", null)
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasySoft.Simple.Tradition.Data.Entities.Blog", "Blog")
                        .WithMany()
                        .HasForeignKey("BlogId1");

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
