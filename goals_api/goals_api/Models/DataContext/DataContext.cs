using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models.DataContext
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<GoalProgress> GoalProgresses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserDescription> UserDescriptions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupGoal> GroupGoals { get; set; }
        public DbSet<GroupGoalProgress> GroupGoalProgresses { get; set; }
        public DbSet<GroupInvitation> GroupInvitations { get; set; }

        // goal day progress
        // goal day progress comments

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goal>()
                .HasOne(g => g.User)
                .WithMany();
            modelBuilder.Entity<GoalProgress>()
                .HasOne(gp => gp.Goal)
                .WithMany();
            modelBuilder.Entity<UserDescription>()
                .HasMany(ud => ud.Comments);
            modelBuilder.Entity<Group>()
                .HasMany(group => group.Members)
                .WithOne();
            modelBuilder.Entity<GroupGoal>()
                .HasOne(goal => goal.Group)
                .WithMany();
            modelBuilder.Entity<GroupGoalProgress>()
                .HasOne(gp => gp.Goal)
                .WithMany();
        }
    }
}
