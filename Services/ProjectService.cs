using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects
                .Include(p => p.ProjectMembers)
                .Include(p => p.Contracts)
                .Include(p => p.Documents)
                .ToListAsync();
        }

        public async Task<Project> GetProjectById(int id)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectMembers)
                    .ThenInclude(pm => pm.User)
                .Include(p => p.Contracts)
                .Include(p => p.Documents)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found");
            }
            return project;
        }

        public async Task<Project> CreateProject(Project project)
        {
            project.CreatedDate = DateTime.UtcNow;
            project.ModifiedDate = DateTime.UtcNow;
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> UpdateProject(int id, Project project)
        {
            var existingProject = await _context.Projects.FindAsync(id);
            if (existingProject == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found");
            }

            existingProject.ProjectCode = project.ProjectCode;
            existingProject.ProjectName = project.ProjectName;
            existingProject.ClientName = project.ClientName;
            existingProject.Status = project.Status;
            existingProject.Description = project.Description;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;
            existingProject.Budget = project.Budget;
            existingProject.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingProject;
        }

        public async Task<bool> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return false;
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
