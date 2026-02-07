using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models
{
    public class Approval
    {
        [Key]
        public int ApprovalId { get; set; }
        public int DocumentId { get; set; }
        public string Status { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public Document Document { get; set; }
    }
}