using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }
        public int ProjectId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;
        public string UploadedBy { get; set; } 
        public string Status { get; set; }
        public int Version { get; set; }
        public string Description { get; set; }
        
        public Project Project { get; set; }
        public ICollection<Approval> Approvals { get; set; }
    }
}