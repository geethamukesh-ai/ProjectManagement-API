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
    public class ContractsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContractsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContracts()
        {
            var contracts = await _context.Contracts
                .Include(c => c.Project)
                .ToListAsync();
            return Ok(contracts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractById(int id)
        {
            var contract = await _context.Contracts
                .Include(c => c.Project)
                .FirstOrDefaultAsync(c => c.ContractId == id);

            if (contract == null)
            {
                return NotFound(new { message = $"Contract with ID {id} not found" });
            }
            return Ok(contract);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetContractsByProjectId(int projectId)
        {
            var contracts = await _context.Contracts
                .Where(c => c.ProjectId == projectId)
                .ToListAsync();
            return Ok(contracts);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateContract([FromBody] Contract contract)
        {
            contract.CreatedDate = DateTime.UtcNow;
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetContractById), new { id = contract.ContractId }, contract);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateContract(int id, [FromBody] Contract contract)
        {
            var existingContract = await _context.Contracts.FindAsync(id);
            if (existingContract == null)
            {
                return NotFound(new { message = $"Contract with ID {id} not found" });
            }

            existingContract.ContractNumber = contract.ContractNumber;
            existingContract.ContractType = contract.ContractType;
            existingContract.Vendor = contract.Vendor;
            existingContract.Amount = contract.Amount;
            existingContract.StartDate = contract.StartDate;
            existingContract.EndDate = contract.EndDate;
            existingContract.Status = contract.Status;
            existingContract.ApprovalStatus = contract.ApprovalStatus;
            existingContract.Terms = contract.Terms;

            await _context.SaveChangesAsync();
            return Ok(existingContract);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound(new { message = $"Contract with ID {id} not found" });
            }

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
