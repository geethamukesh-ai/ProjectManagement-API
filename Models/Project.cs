using System;
using System.Collections.Generic;

namespace ProjectManagementAPI.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}