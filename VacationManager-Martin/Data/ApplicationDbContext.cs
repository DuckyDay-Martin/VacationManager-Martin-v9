using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;
using VacationManager_Martin.Data.Entities;
using VacationManager_Martin.Data.Entities.TimeOffs;
using VacationManager_Martin.Models;

namespace VacationManager_Martin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasOne(u => u.Team)
            .WithMany(t => t.Developers)
            .OnDelete(DeleteBehavior.Restrict);
            

            modelBuilder.Entity<Team>()
                .HasOne(t => t.TeamLead)
                .WithMany(u => u.LedTeams)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Teams)
                .WithOne(t => t.Project)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.PaidTimeOffRequests)
            //    .WithRequired(pto => pto.Requestor)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.UnpaidTimeOffRequests)
            //    .WithRequired(uto => uto.Requestor)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.SickTimeOffRequests)
            //    .WithRequired(sto => sto.Requestor)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Role>()
            //    .HasMany(r => r.Users)
            //    .WithRequired(u => u.Role)
            //    .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public  virtual DbSet<Project> Projects { get; set; }
        public  virtual DbSet<PaidTimeOff> PaidTimeOffs { get; set; }
        public virtual DbSet<UnpaidTimeOff> UnpaidTimeOffs { get; set; }
        public virtual DbSet<SickTimeOff> SickTimeOffs { get; set;}
       // public DbSet<VacationManager_Martin.Models.UserViewModel> UserViewModel { get; set; } = default!;
    }
}