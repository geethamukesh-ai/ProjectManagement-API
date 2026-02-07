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
    public class DocumentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var documents = await _context.Documents
                .Include(d => d.Project)
                .ToListAsync();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var document = await _context.Documents
                .Include(d => d.Project)
                .Include(d => d.Approvals)
                .FirstOrDefaultAsync(d => d.DocumentId == id);

            if (document == null)
            {
                return NotFound(new { message = $"Document with ID {id} not found" });
            }
            return Ok(document);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetDocumentsByProjectId(int projectId)
        {
            var documents = await _context.Documents
                .Where(d => d.ProjectId == projectId)
                .ToListAsync();
            return Ok(documents);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager,User")]
        public async Task<IActionResult> CreateDocument([FromBody] Document document)
        {
            document.UploadedDate = DateTime.UtcNow;
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDocumentById), new { id = document.DocumentId }, document);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] Document document)
        {
            var existingDocument = await _context.Documents.FindAsync(id);
            if (existingDocument == null)
            {
                return NotFound(new { message = $"Document with ID {id} not found" });
            }

            existingDocument.FileName = document.FileName;
            existingDocument.FilePath = document.FilePath;
            existingDocument.FileSize = document.FileSize;
            existingDocument.Status = document.Status;
            existingDocument.Version = document.Version;
            existingDocument.Description = document.Description;

            await _context.SaveChangesAsync();
            return Ok(existingDocument);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound(new { message = $"Document with ID {id} not found" });
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
