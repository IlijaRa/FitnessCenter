using FitnessCenterLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace FitnessCenterMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //This code makes composite keys
            modelBuilder.Entity<FitnessMemberWorkout>().HasKey(sc => new { sc.FitnessCenterMemberId, sc.WorkoutId });
            modelBuilder.Entity<FitnessCenterHall>().HasKey(sc => new { sc.HallId, sc.FitnessCenterId });
            modelBuilder.Entity<Term>().HasKey(sc => new { sc.CoachId, sc.WorkoutId });
            modelBuilder.Entity<Schedule>().HasKey(sc => new { sc.FitnessCenterHallId, sc.TermId });

            // Set the default table name of AspNetUsers(for example when you want to register User )
            modelBuilder.Entity<User>(config => { config.ToTable("AspNetUsers"); });

            // Defines values for Discriminator column
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<FitnessCenterMember>("FitnessCenterMember")
                .HasValue<Coach>("Coach")
                .HasValue<Administrator>("Administrator");

        }

        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Coach> Coach { get; set; }
        public DbSet<FitnessCenter> FitnessCenter { get; set; }
        public DbSet<FitnessCenterHall> FitnessCenterHall { get; set; }
        public DbSet<FitnessCenterMember> FitnessCenterMember { get; set; }
        public DbSet<FitnessMemberWorkout> FitnessMemberWorkout { get; set; }
        public DbSet<Hall> Hall { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Term> Term { get; set; }
        //public DbSet<User> User { get; set; }
        public DbSet<Workout> Workout { get; set; }
    }
}