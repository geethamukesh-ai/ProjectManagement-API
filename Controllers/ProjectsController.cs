using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Models;
using ProjectManagementAPI.Services;

namespace ProjectManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjects();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            try
            {
                var project = await _projectService.GetProjectById(id);
                return Ok(project);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            var createdProject = await _projectService.CreateProject(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.ProjectId }, createdProject);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] Project project)
        {
            try
            {
                var updatedProject = await _projectService.UpdateProject(id, project);
                return Ok(updatedProject);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProject(id);
            if (!result)
            {
                return NotFound(new { message = $"Project with ID {id} not found" });
            }
            return NoContent();
        }
    }
}
