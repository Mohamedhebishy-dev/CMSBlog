using System.Diagnostics.CodeAnalysis;

namespace CMSBlog.Areas.Admin.Models
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [AllowNull]
        public string? NewPassword { get; set; } 
        public List<string> UserRoles { get; set; } = new List<string>();
        public List<string> AvailableRoles { get; set; } = new List<string>();
        public string RolesToAdd { get; set; }
        public List<string> RolesToRemove { get; set; } = new List<string>();
    }
}
