using Microsoft.EntityFrameworkCore;
using ProjectManagement_API.Models;

namespace ProjectManagement_API.Data
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

            // Seed data configuration
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin", Email = "admin@example.com" },
                new User { Id = 2, Name = "User1", Email = "user1@example.com" }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, Name = "Project Alpha", Description = "Description for Project Alpha" }
            );

            modelBuilder.Entity<ProjectMember>().HasData(
                new ProjectMember { Id = 1, ProjectId = 1, UserId = 1 }
            );

            modelBuilder.Entity<Contract>().HasData(
                new Contract { Id = 1, ProjectId = 1, ContractDetails = "Contract details for Project Alpha" }
            );

            modelBuilder.Entity<Document>().HasData(
                new Document { Id = 1, ProjectId = 1, FileName = "Document1.pdf" }
            );

            modelBuilder.Entity<Approval>().HasData(
                new Approval { Id = 1, DocumentId = 1, Approved = true }
            );
        }
    }
}