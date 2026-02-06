using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models
{
    public class Contract
    {
        [Key]
        public int ContractId { get; set; }
        public int ProjectId { get; set; }
        public string ContractNumber { get; set; }
        public string ContractType { get; set; }
        public string Vendor { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string ApprovalStatus { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedDate { get; set; }
        public string Terms { get; set; }
        
        public Project Project { get; set; }
    }
}