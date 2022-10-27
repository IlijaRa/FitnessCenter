﻿// <auto-generated />
using System;
using FitnessCenterMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FitnessCenterMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221027215417_added-tables")]
    partial class addedtables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FitnessCenterLibrary.Models.FitnessCenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FitnessCenter");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.FitnessCenterHall", b =>
                {
                    b.Property<int>("HallId")
                        .HasColumnType("int");

                    b.Property<int>("FitnessCenterId")
                        .HasColumnType("int");

                    b.HasKey("HallId", "FitnessCenterId");

                    b.HasIndex("FitnessCenterId");

                    b.HasIndex("HallId")
                        .IsUnique();

                    b.ToTable("FitnessCenterHall");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.FitnessMemberWorkout", b =>
                {
                    b.Property<string>("FitnessCenterMemberId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("FitnessCenterMemberId", "WorkoutId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("FitnessMemberWorkout");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Hall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("FitnessCenterId")
                        .HasColumnType("int");

                    b.Property<string>("HallMark")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("FitnessCenterId");

                    b.ToTable("Hall");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Schedule", b =>
                {
                    b.Property<int>("FitnessCenterHallId")
                        .HasColumnType("int");

                    b.Property<int>("TermId")
                        .HasColumnType("int");

                    b.Property<int>("FitnessCenterHallFitnessCenterId")
                        .HasColumnType("int");

                    b.Property<int>("FitnessCenterHallHallId")
                        .HasColumnType("int");

                    b.Property<string>("TermCoachId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TermWorkoutId")
                        .HasColumnType("int");

                    b.HasKey("FitnessCenterHallId", "TermId");

                    b.HasIndex("FitnessCenterHallHallId", "FitnessCenterHallFitnessCenterId");

                    b.HasIndex("TermCoachId", "TermWorkoutId");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Term", b =>
                {
                    b.Property<string>("CoachId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfMembers")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("CoachId", "WorkoutId");

                    b.HasIndex("WorkoutId")
                        .IsUnique();

                    b.ToTable("Term");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CoachId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.ToTable("Workout");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Administrator", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<DateTime>("EmploymentDay")
                        .HasColumnType("datetime2");

                    b.Property<int>("YearsOfExperience")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Coach", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<int>("FitnessCenterId")
                        .HasColumnType("int");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasIndex("FitnessCenterId");

                    b.HasDiscriminator().HasValue("Coach");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.FitnessCenterMember", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<DateTime>("FirstMembership")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("FitnessCenterMember");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.FitnessCenterHall", b =>
                {
                    b.HasOne("FitnessCenterLibrary.Models.FitnessCenter", "FitnessCenter")
                        .WithMany("FitnessCenterHalls")
                        .HasForeignKey("FitnessCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessCenterLibrary.Models.Hall", "Hall")
                        .WithOne("FitnessCenterHall")
                        .HasForeignKey("FitnessCenterLibrary.Models.FitnessCenterHall", "HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FitnessCenter");

                    b.Navigation("Hall");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.FitnessMemberWorkout", b =>
                {
                    b.HasOne("FitnessCenterLibrary.Models.FitnessCenterMember", "FitnessCenterMember")
                        .WithMany("FitnessMemberWorkouts")
                        .HasForeignKey("FitnessCenterMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessCenterLibrary.Models.Workout", "Workout")
                        .WithMany("FitnessMemberWorkouts")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FitnessCenterMember");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Hall", b =>
                {
                    b.HasOne("FitnessCenterLibrary.Models.FitnessCenter", "FitnessCenter")
                        .WithMany("Halls")
                        .HasForeignKey("FitnessCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FitnessCenter");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Schedule", b =>
                {
                    b.HasOne("FitnessCenterLibrary.Models.FitnessCenterHall", "FitnessCenterHall")
                        .WithMany("Schedules")
                        .HasForeignKey("FitnessCenterHallHallId", "FitnessCenterHallFitnessCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessCenterLibrary.Models.Term", "Term")
                        .WithMany("Schedules")
                        .HasForeignKey("TermCoachId", "TermWorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FitnessCenterHall");

                    b.Navigation("Term");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Term", b =>
                {
                    b.HasOne("FitnessCenterLibrary.Models.Coach", "Coach")
                        .WithMany("Terms")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessCenterLibrary.Models.Workout", "Workout")
                        .WithOne("Term")
                        .HasForeignKey("FitnessCenterLibrary.Models.Term", "WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coach");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Workout", b =>
                {
                    b.HasOne("FitnessCenterLibrary.Models.Coach", "Coach")
                        .WithMany("Workouts")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Coach", b =>
                {
                    b.HasOne("FitnessCenterLibrary.Models.FitnessCenter", "FitnessCenter")
                        .WithMany("Coaches")
                        .HasForeignKey("FitnessCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FitnessCenter");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.FitnessCenter", b =>
                {
                    b.Navigation("Coaches");

                    b.Navigation("FitnessCenterHalls");

                    b.Navigation("Halls");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.FitnessCenterHall", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Hall", b =>
                {
                    b.Navigation("FitnessCenterHall")
                        .IsRequired();
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Term", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Workout", b =>
                {
                    b.Navigation("FitnessMemberWorkouts");

                    b.Navigation("Term")
                        .IsRequired();
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.Coach", b =>
                {
                    b.Navigation("Terms");

                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("FitnessCenterLibrary.Models.FitnessCenterMember", b =>
                {
                    b.Navigation("FitnessMemberWorkouts");
                });
#pragma warning restore 612, 618
        }
    }
}