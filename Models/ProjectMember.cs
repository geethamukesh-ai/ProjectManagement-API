using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models
{
    public class ProjectMember
    {
        [Key]
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public string ApprovalStatus { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        
        public Project Project { get; set; }
        public User User { get; set; }
    }
}