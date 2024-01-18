﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using deskManagerApi.Entities;

#nullable disable

namespace deskManagerApi.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20240114201610_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("deskManagerApi.Entities.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeskId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeskId");

                    b.HasIndex("UserId");

                    b.ToTable("reservations");
                });

            modelBuilder.Entity("deskManagerApi.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("brands");
                });

            modelBuilder.Entity("deskManagerApi.Models.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("buildings");
                });

            modelBuilder.Entity("deskManagerApi.Models.Desk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Height")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapXLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapYLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Width")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("StatusId");

                    b.ToTable("desks");
                });

            modelBuilder.Entity("deskManagerApi.Models.DeskStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("deskStatuses");
                });

            modelBuilder.Entity("deskManagerApi.Models.DesksTeams", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DeskId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeskId");

                    b.HasIndex("TeamId");

                    b.ToTable("deskTeams");
                });

            modelBuilder.Entity("deskManagerApi.Models.Floor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BuildingId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("floors");
                });

            modelBuilder.Entity("deskManagerApi.Models.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<int?>("DeskId")
                        .HasColumnType("int");

                    b.Property<int?>("ReporterId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeskId");

                    b.HasIndex("ReporterId");

                    b.ToTable("issues");
                });

            modelBuilder.Entity("deskManagerApi.Models.IssueHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ChangedBy")
                        .HasColumnType("int");

                    b.Property<int?>("IssueId")
                        .HasColumnType("int");

                    b.Property<int>("StatusFrom")
                        .HasColumnType("int");

                    b.Property<int>("StatusTo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChangedBy");

                    b.HasIndex("IssueId");

                    b.ToTable("issueHistories");
                });

            modelBuilder.Entity("deskManagerApi.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int?>("DeskId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("DeskId");

                    b.HasIndex("OwnerId");

                    b.ToTable("items");
                });

            modelBuilder.Entity("deskManagerApi.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("FloorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("mapHeight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mapViewBox")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mapWidth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mapXmlns")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.ToTable("rooms");
                });

            modelBuilder.Entity("deskManagerApi.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("teams");
                });

            modelBuilder.Entity("deskManagerApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("deskManagerApi.Entities.Models.Reservation", b =>
                {
                    b.HasOne("deskManagerApi.Models.Desk", "Desk")
                        .WithMany("Reservations")
                        .HasForeignKey("DeskId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("deskManagerApi.Models.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Desk");

                    b.Navigation("User");
                });

            modelBuilder.Entity("deskManagerApi.Models.Desk", b =>
                {
                    b.HasOne("deskManagerApi.Models.Room", "Room")
                        .WithMany("Desks")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("deskManagerApi.Models.DeskStatus", "Status")
                        .WithMany("Desks")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Room");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("deskManagerApi.Models.DesksTeams", b =>
                {
                    b.HasOne("deskManagerApi.Models.Desk", "Desk")
                        .WithMany("DesksTeams")
                        .HasForeignKey("DeskId");

                    b.HasOne("deskManagerApi.Models.Team", "Team")
                        .WithMany("DesksTeams")
                        .HasForeignKey("TeamId");

                    b.Navigation("Desk");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("deskManagerApi.Models.Floor", b =>
                {
                    b.HasOne("deskManagerApi.Models.Building", "Building")
                        .WithMany("Floors")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Building");
                });

            modelBuilder.Entity("deskManagerApi.Models.Issue", b =>
                {
                    b.HasOne("deskManagerApi.Models.Desk", "Desk")
                        .WithMany("Issues")
                        .HasForeignKey("DeskId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("deskManagerApi.Models.User", "Reporter")
                        .WithMany("Issues")
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Desk");

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("deskManagerApi.Models.IssueHistory", b =>
                {
                    b.HasOne("deskManagerApi.Models.User", "User")
                        .WithMany("IssueHistories")
                        .HasForeignKey("ChangedBy")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("deskManagerApi.Models.Issue", "Issue")
                        .WithMany("History")
                        .HasForeignKey("IssueId");

                    b.Navigation("Issue");

                    b.Navigation("User");
                });

            modelBuilder.Entity("deskManagerApi.Models.Item", b =>
                {
                    b.HasOne("deskManagerApi.Models.Brand", "Brand")
                        .WithMany("Items")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("deskManagerApi.Models.Desk", "Desk")
                        .WithMany("Items")
                        .HasForeignKey("DeskId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("deskManagerApi.Models.User", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Brand");

                    b.Navigation("Desk");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("deskManagerApi.Models.Room", b =>
                {
                    b.HasOne("deskManagerApi.Models.Floor", "Floor")
                        .WithMany("Rooms")
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Floor");
                });

            modelBuilder.Entity("deskManagerApi.Models.User", b =>
                {
                    b.HasOne("deskManagerApi.Models.Team", "Team")
                        .WithMany("Users")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Team");
                });

            modelBuilder.Entity("deskManagerApi.Models.Brand", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("deskManagerApi.Models.Building", b =>
                {
                    b.Navigation("Floors");
                });

            modelBuilder.Entity("deskManagerApi.Models.Desk", b =>
                {
                    b.Navigation("DesksTeams");

                    b.Navigation("Issues");

                    b.Navigation("Items");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("deskManagerApi.Models.DeskStatus", b =>
                {
                    b.Navigation("Desks");
                });

            modelBuilder.Entity("deskManagerApi.Models.Floor", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("deskManagerApi.Models.Issue", b =>
                {
                    b.Navigation("History");
                });

            modelBuilder.Entity("deskManagerApi.Models.Room", b =>
                {
                    b.Navigation("Desks");
                });

            modelBuilder.Entity("deskManagerApi.Models.Team", b =>
                {
                    b.Navigation("DesksTeams");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("deskManagerApi.Models.User", b =>
                {
                    b.Navigation("IssueHistories");

                    b.Navigation("Issues");

                    b.Navigation("Items");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
