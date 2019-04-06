﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using goals_api.Models.DataContext;

namespace goals_api.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("goals_api.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<int>("CommentUserDescriptionId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("UserDescriptionId");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserDescriptionId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("goals_api.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("Username");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("goals_api.Models.GoalProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("GoalId");

                    b.Property<bool>("IsDone");

                    b.HasKey("Id");

                    b.HasIndex("GoalId");

                    b.ToTable("GoalProgresses");
                });

            modelBuilder.Entity("goals_api.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("GroupName");

                    b.Property<string>("LeaderUsername");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("goals_api.Models.GroupGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("GroupId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupGoals");
                });

            modelBuilder.Entity("goals_api.Models.GroupGoalProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("GoalId");

                    b.Property<bool>("IsDone");

                    b.Property<string>("MemberUsername")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GoalId");

                    b.ToTable("GroupGoalProgresses");
                });

            modelBuilder.Entity("goals_api.Models.GroupInvitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateAt");

                    b.Property<string>("LeaderUsername");

                    b.Property<string>("MemberUsername");

                    b.HasKey("Id");

                    b.ToTable("GroupInvitations");
                });

            modelBuilder.Entity("goals_api.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("GroupId");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Token");

                    b.HasKey("Username");

                    b.HasIndex("GroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("goals_api.Models.UserDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateAt");

                    b.Property<string>("Description");

                    b.Property<string>("username");

                    b.HasKey("Id");

                    b.ToTable("UserDescriptions");
                });

            modelBuilder.Entity("goals_api.Models.Comment", b =>
                {
                    b.HasOne("goals_api.Models.UserDescription")
                        .WithMany("Comments")
                        .HasForeignKey("UserDescriptionId");
                });

            modelBuilder.Entity("goals_api.Models.Goal", b =>
                {
                    b.HasOne("goals_api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Username");
                });

            modelBuilder.Entity("goals_api.Models.GoalProgress", b =>
                {
                    b.HasOne("goals_api.Models.Goal", "Goal")
                        .WithMany()
                        .HasForeignKey("GoalId");
                });

            modelBuilder.Entity("goals_api.Models.GroupGoal", b =>
                {
                    b.HasOne("goals_api.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("goals_api.Models.GroupGoalProgress", b =>
                {
                    b.HasOne("goals_api.Models.GroupGoal", "Goal")
                        .WithMany()
                        .HasForeignKey("GoalId");
                });

            modelBuilder.Entity("goals_api.Models.User", b =>
                {
                    b.HasOne("goals_api.Models.Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupId");
                });
#pragma warning restore 612, 618
        }
    }
}
