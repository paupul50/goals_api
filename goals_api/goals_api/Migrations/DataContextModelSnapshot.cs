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

                    b.Property<string>("AuthorUsernameUsername");

                    b.Property<string>("Body");

                    b.Property<string>("CommentedUserUsername");

                    b.Property<DateTime>("CreatedAt");

                    b.HasKey("Id");

                    b.HasIndex("AuthorUsernameUsername");

                    b.HasIndex("CommentedUserUsername");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("goals_api.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("GoalMediumId");

                    b.Property<int>("GoalNumberValue");

                    b.Property<string>("GoalStringValue");

                    b.Property<int>("GoalType");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int?>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("GoalMediumId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("goals_api.Models.GoalProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("GoalId");

                    b.Property<int>("GoalNumberValue");

                    b.Property<string>("GoalStringValue");

                    b.Property<bool>("IsDone");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("GoalId");

                    b.HasIndex("Username");

                    b.ToTable("GoalProgresses");
                });

            modelBuilder.Entity("goals_api.Models.Goals.GoalMedium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GroupId");

                    b.Property<bool>("IsGroupMedium");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("Username");

                    b.ToTable("GoalMedia");
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

            modelBuilder.Entity("goals_api.Models.GroupInvitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateAt");

                    b.Property<int?>("GroupId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("Username");

                    b.ToTable("GroupInvitations");
                });

            modelBuilder.Entity("goals_api.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<string>("Firstname")
                        .IsRequired();

                    b.Property<string>("GoogleToken");

                    b.Property<int?>("GroupId");

                    b.Property<string>("Image");

                    b.Property<string>("Lastname")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Token");

                    b.HasKey("Username");

                    b.HasIndex("GroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("goals_api.Models.Workouts.RoutePoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CircleDraggable");

                    b.Property<bool>("Editable");

                    b.Property<string>("FillColour");

                    b.Property<int>("Index");

                    b.Property<double>("Lat");

                    b.Property<double>("Lng");

                    b.Property<double>("Radius");

                    b.Property<int?>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("RoutePoints");
                });

            modelBuilder.Entity("goals_api.Models.Workouts.RoutePointProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDone");

                    b.Property<int?>("RoutePointId");

                    b.Property<int?>("WorkoutProgressId");

                    b.HasKey("Id");

                    b.HasIndex("RoutePointId");

                    b.HasIndex("WorkoutProgressId");

                    b.ToTable("RoutePointProgresses");
                });

            modelBuilder.Entity("goals_api.Models.Workouts.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatorUsername");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CreatorUsername");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("goals_api.Models.Workouts.WorkoutProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDone");

                    b.Property<int>("ProgressIndex");

                    b.Property<string>("Username");

                    b.Property<int?>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("Username");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutProgresses");
                });

            modelBuilder.Entity("goals_api.Models.Comment", b =>
                {
                    b.HasOne("goals_api.Models.User", "AuthorUsername")
                        .WithMany()
                        .HasForeignKey("AuthorUsernameUsername");

                    b.HasOne("goals_api.Models.User", "CommentedUser")
                        .WithMany()
                        .HasForeignKey("CommentedUserUsername");
                });

            modelBuilder.Entity("goals_api.Models.Goal", b =>
                {
                    b.HasOne("goals_api.Models.Goals.GoalMedium", "GoalMedium")
                        .WithMany()
                        .HasForeignKey("GoalMediumId");

                    b.HasOne("goals_api.Models.Workouts.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("goals_api.Models.GoalProgress", b =>
                {
                    b.HasOne("goals_api.Models.Goal", "Goal")
                        .WithMany()
                        .HasForeignKey("GoalId");

                    b.HasOne("goals_api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Username");
                });

            modelBuilder.Entity("goals_api.Models.Goals.GoalMedium", b =>
                {
                    b.HasOne("goals_api.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("goals_api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Username");
                });

            modelBuilder.Entity("goals_api.Models.GroupInvitation", b =>
                {
                    b.HasOne("goals_api.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("goals_api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Username");
                });

            modelBuilder.Entity("goals_api.Models.User", b =>
                {
                    b.HasOne("goals_api.Models.Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("goals_api.Models.Workouts.RoutePoint", b =>
                {
                    b.HasOne("goals_api.Models.Workouts.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("goals_api.Models.Workouts.RoutePointProgress", b =>
                {
                    b.HasOne("goals_api.Models.Workouts.RoutePoint", "RoutePoint")
                        .WithMany()
                        .HasForeignKey("RoutePointId");

                    b.HasOne("goals_api.Models.Workouts.WorkoutProgress", "WorkoutProgress")
                        .WithMany()
                        .HasForeignKey("WorkoutProgressId");
                });

            modelBuilder.Entity("goals_api.Models.Workouts.Workout", b =>
                {
                    b.HasOne("goals_api.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorUsername");
                });

            modelBuilder.Entity("goals_api.Models.Workouts.WorkoutProgress", b =>
                {
                    b.HasOne("goals_api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Username");

                    b.HasOne("goals_api.Models.Workouts.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId");
                });
#pragma warning restore 612, 618
        }
    }
}
