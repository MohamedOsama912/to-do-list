﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using to_do_list_project.Data;

#nullable disable

namespace to_do_list_project.Migrations
{
    [DbContext(typeof(ToDoContext))]
    [Migration("20240510185807_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("to_do_list_project.Models.Catigory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Catigories");

                    b.HasData(
                        new
                        {
                            Id = "work",
                            Name = "Work"
                        },
                        new
                        {
                            Id = "home",
                            Name = "Home"
                        },
                        new
                        {
                            Id = "ex",
                            Name = "Exercise"
                        },
                        new
                        {
                            Id = "shop",
                            Name = "Shopping"
                        },
                        new
                        {
                            Id = "call",
                            Name = "Contact"
                        });
                });

            modelBuilder.Entity("to_do_list_project.Models.Status", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = "open",
                            Name = "Open"
                        },
                        new
                        {
                            Id = "closed",
                            Name = "Completed"
                        });
                });

            modelBuilder.Entity("to_do_list_project.Models.ToDo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CatigoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Descriotion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StatusId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CatigoryId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("ToDos");
                });

            modelBuilder.Entity("to_do_list_project.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicturePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepeatPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("to_do_list_project.Models.ToDo", b =>
                {
                    b.HasOne("to_do_list_project.Models.Catigory", "Catigory")
                        .WithMany("ToDos")
                        .HasForeignKey("CatigoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("to_do_list_project.Models.Status", "Status")
                        .WithMany("ToDos")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("to_do_list_project.Models.User", "User")
                        .WithMany("ToDos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catigory");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("to_do_list_project.Models.Catigory", b =>
                {
                    b.Navigation("ToDos");
                });

            modelBuilder.Entity("to_do_list_project.Models.Status", b =>
                {
                    b.Navigation("ToDos");
                });

            modelBuilder.Entity("to_do_list_project.Models.User", b =>
                {
                    b.Navigation("ToDos");
                });
#pragma warning restore 612, 618
        }
    }
}
