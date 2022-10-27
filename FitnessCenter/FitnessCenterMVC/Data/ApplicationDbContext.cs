using FitnessCenterLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*This code makes composite keys*/
            modelBuilder.Entity<FitnessMemberWorkout>().HasKey(sc => new { sc.FitnessCenterMemberId, sc.WorkoutId });
            modelBuilder.Entity<FitnessCenterHall>().HasKey(sc => new { sc.HallId, sc.FitnessCenterId });
            modelBuilder.Entity<Term>().HasKey(sc => new { sc.CoachId, sc.WorkoutId });
            modelBuilder.Entity<Schedule>().HasKey(sc => new { sc.FitnessCenterHallId, sc.TermId });

            //modelBuilder.Entity<User>()
            //.HasOne<Coach>(s => s.Coach)
            //.WithOne(ad => ad.User)
            //.HasForeignKey<Coach>(ad => ad.UserId);

            //modelBuilder.Entity<User>()
            //.HasOne<FitnessCenterMember>(s => s.FitnessCenterMember)
            //.WithOne(ad => ad.User)
            //.HasForeignKey<FitnessCenterMember>(ad => ad.UserId);

            //modelBuilder.Entity<User>()
            //.HasOne<Administrator>(s => s.Administrator)
            //.WithOne(ad => ad.User)
            //.HasForeignKey<Administrator>(ad => ad.UserId);
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