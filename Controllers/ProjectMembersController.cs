using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectMembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjectMembers()
        {
            var members = await _context.ProjectMembers
                .Include(pm => pm.Project)
                .Include(pm => pm.User)
                .ToListAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectMemberById(int id)
        {
            var member = await _context.ProjectMembers
                .Include(pm => pm.Project)
                .Include(pm => pm.User)
                .FirstOrDefaultAsync(pm => pm.MemberId == id);

            if (member == null)
            {
                return NotFound(new { message = $"Project member with ID {id} not found" });
            }
            return Ok(member);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetMembersByProjectId(int projectId)
        {
            var members = await _context.ProjectMembers
                .Include(pm => pm.User)
                .Where(pm => pm.ProjectId == projectId)
                .ToListAsync();
            return Ok(members);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> AddProjectMember([FromBody] ProjectMember member)
        {
            member.JoinDate = DateTime.UtcNow;
            _context.ProjectMembers.Add(member);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProjectMemberById), new { id = member.MemberId }, member);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateProjectMember(int id, [FromBody] ProjectMember member)
        {
            var existingMember = await _context.ProjectMembers.FindAsync(id);
            if (existingMember == null)
            {
                return NotFound(new { message = $"Project member with ID {id} not found" });
            }

            existingMember.Role = member.Role;
            existingMember.ApprovalStatus = member.ApprovalStatus;
            existingMember.ApprovedDate = member.ApprovedDate;
            existingMember.ApprovedBy = member.ApprovedBy;

            await _context.SaveChangesAsync();
            return Ok(existingMember);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> RemoveProjectMember(int id)
        {
            var member = await _context.ProjectMembers.FindAsync(id);
            if (member == null)
            {
                return NotFound(new { message = $"Project member with ID {id} not found" });
            }

            _context.ProjectMembers.Remove(member);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
