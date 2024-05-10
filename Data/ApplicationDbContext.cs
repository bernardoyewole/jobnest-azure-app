using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobNest.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:jobnest.database.windows.net,1433;Initial Catalog=JobnestDB;Persist Security Info=False;User ID=CloudSA74dce76b;Password=Bernard2002!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<IdentityRole> Roles { get; set; }
    }
}
