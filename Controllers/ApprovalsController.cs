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
    public class ApprovalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApprovalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApprovals()
        {
            var approvals = await _context.Approvals
                .Include(a => a.Document)
                .ToListAsync();
            return Ok(approvals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApprovalById(int id)
        {
            var approval = await _context.Approvals
                .Include(a => a.Document)
                .FirstOrDefaultAsync(a => a.ApprovalId == id);

            if (approval == null)
            {
                return NotFound(new { message = $"Approval with ID {id} not found" });
            }
            return Ok(approval);
        }

        [HttpGet("document/{documentId}")]
        public async Task<IActionResult> GetApprovalsByDocumentId(int documentId)
        {
            var approvals = await _context.Approvals
                .Where(a => a.DocumentId == documentId)
                .ToListAsync();
            return Ok(approvals);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateApproval([FromBody] Approval approval)
        {
            approval.CreatedDate = DateTime.UtcNow;
            _context.Approvals.Add(approval);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetApprovalById), new { id = approval.ApprovalId }, approval);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateApproval(int id, [FromBody] Approval approval)
        {
            var existingApproval = await _context.Approvals.FindAsync(id);
            if (existingApproval == null)
            {
                return NotFound(new { message = $"Approval with ID {id} not found" });
            }

            existingApproval.Status = approval.Status;
            existingApproval.ApprovedBy = approval.ApprovedBy;
            existingApproval.ApprovalDate = approval.ApprovalDate;
            existingApproval.Comments = approval.Comments;

            await _context.SaveChangesAsync();
            return Ok(existingApproval);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteApproval(int id)
        {
            var approval = await _context.Approvals.FindAsync(id);
            if (approval == null)
            {
                return NotFound(new { message = $"Approval with ID {id} not found" });
            }

            _context.Approvals.Remove(approval);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
