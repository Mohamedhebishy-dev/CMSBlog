namespace CMSBlog.Areas.Admin.Models
{
    public class UserDetailsViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool EmailConfirmed { get; set; }
        public string ImageUser { get; set; }
        public List<string> Roles { get; set; }
    }
}
