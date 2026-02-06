using System;

namespace ProjectManagementAPI.Models
{
    public class Approval
    {
        public int ApprovalId { get; set; }
        public string EntityType { get; set; }
        public int EntityId { get; set; }
        public string Status { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}