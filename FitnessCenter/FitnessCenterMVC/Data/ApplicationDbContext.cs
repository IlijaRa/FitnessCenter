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

        /*
            ---Inputs for Package Manager Console---
            1. Add-Migration "Migration name" [-context -DbContextName]
            2. Update-Database
         */      
        public DbSet<Address> Address { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Coach> Coach { get; set; }
        public DbSet<FitnessCenter> FitnessCenter { get; set; }
        public DbSet<FitnessCenterMember> FitnessCenterMember { get; set; }
        public DbSet<Hall> Hall { get; set; }
        public DbSet<Rate> Rate { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Workout> Workout { get; set; }
    }
}