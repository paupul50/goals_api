using goals_api.Models.Goals;
using goals_api.Models.Workouts;
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
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupInvitation> GroupInvitations { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<RoutePoint> RoutePoints { get; set; }
        public DbSet<WorkoutProgress> WorkoutProgresses { get; set; }
        public DbSet<RoutePointProgress> RoutePointProgresses { get; set; }
        public DbSet<GoalMedium> GoalMedia { get; set; }

        // goal day progress
        // goal day progress comments

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.AuthorUsername)
                .WithMany();
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.CommentedUser)
                .WithMany();

            modelBuilder.Entity<WorkoutProgress>()
                .HasOne(wp => wp.Workout)
                .WithMany();
            modelBuilder.Entity<WorkoutProgress>()
                .HasOne(wp => wp.User)
                .WithMany();
            modelBuilder.Entity<Workout>()
                .HasOne(w => w.Creator)
                .WithMany();
            modelBuilder.Entity<RoutePointProgress>()
                .HasOne(wpp => wpp.RoutePoint)
                .WithMany();
            modelBuilder.Entity<RoutePointProgress>()
                .HasOne(wpp => wpp.WorkoutProgress)
                .WithMany();
            modelBuilder.Entity<RoutePoint>()
                .HasOne(rp => rp.Workout)
                .WithMany();

            modelBuilder.Entity<GoalMedium>()
                .HasOne(gm => gm.Group)
                .WithMany();
            modelBuilder.Entity<GoalMedium>()
                .HasOne(gm => gm.User)
                .WithMany();

            modelBuilder.Entity<GroupInvitation>()
                .HasOne(gi => gi.User)
                .WithMany();
            modelBuilder.Entity<GroupInvitation>()
                .HasOne(gi => gi.Group)
                .WithMany();

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Members)
                .WithOne();

            modelBuilder.Entity<GoalProgress>()
                .HasOne(gp => gp.User)
                .WithMany();
            modelBuilder.Entity<GoalProgress>()
                .HasOne(gp => gp.Goal)
                .WithMany();

            modelBuilder.Entity<Goal>()
                .HasOne(gp => gp.Workout)
                .WithMany();
            modelBuilder.Entity<Goal>()
                .HasOne(gp => gp.GoalMedium)
                .WithMany();
        }
    }
}
