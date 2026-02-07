using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Approval> Approvals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Project>()
                .HasMany(p => p.ProjectMembers)
                .WithOne(pm => pm.Project)
                .HasForeignKey(pm => pm.ProjectId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Documents)
                .WithOne(d => d.Project)
                .HasForeignKey(d => d.ProjectId);

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.ProjectMembers)
                .HasForeignKey(pm => pm.UserId);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Project)
                .WithMany(p => p.Contracts)
                .HasForeignKey(c => c.ProjectId);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Project)
                .WithMany(p => p.Documents)
                .HasForeignKey(d => d.ProjectId);

            modelBuilder.Entity<Approval>()
                .HasOne(a => a.Document)
                .WithMany(d => d.Approvals)
                .HasForeignKey(a => a.DocumentId);
        }
    }
}