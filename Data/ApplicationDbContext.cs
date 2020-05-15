//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserLogOnSecurityTools.Models;
//using Microsoft.EntityFrameworkCore.Relational;

namespace UserLogOnSecurityTools.Data
{
    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> GeocortexUsers { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Membership> Memberships { get; set; }
        public DbSet<UserAction> UserActions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Nike;Initial Catalog=GeocortexUsers;Integrated Security=False;User ID=sa;Password=5ucce55!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            // One-to-one relationship between users and memberships
            modelBuilder.Entity<User>()
                .HasOne(u => u.Membership)
                .WithOne(m => m.User)
                .HasForeignKey<Membership>(m => m.UserId);

            // One-to-many relationship between users and user actions
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserActions)
                .WithOne(m => m.User);

            // Composite key for UserRoles
            modelBuilder.Entity<UserRole>().HasKey(x => new {x.UserId, x.RoleId });
        }
    }
}
