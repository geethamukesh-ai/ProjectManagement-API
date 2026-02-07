using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ProjectCode { get; set; }
        
        [Required]
        [StringLength(200)]
        public string ProjectName { get; set; }
        
        [StringLength(200)]
        public string ClientName { get; set; }
        
        [StringLength(50)]
        public string Status { get; set; }
        
        public string Description { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
        
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}