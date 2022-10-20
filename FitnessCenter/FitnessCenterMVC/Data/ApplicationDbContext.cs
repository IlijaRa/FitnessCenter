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
            1. Add-Migration "Migration name" [-context -DbContextName]
            2. Update-Database
         */
        public DbSet<User> User { get; set; }
        // TODO: generate other tables
    }
}