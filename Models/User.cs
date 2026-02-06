using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_API.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Role { get; set; } // Admin, Manager, User

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; }

        public ICollection<ProjectMember> ProjectMembers { get; set; }
    }
}