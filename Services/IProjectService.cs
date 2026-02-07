using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectById(int id);
        Task<Project> CreateProject(Project project);
        Task<Project> UpdateProject(int id, Project project);
        Task<bool> DeleteProject(int id);
    }
}
