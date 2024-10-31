using CMSBlog.Resources;
using System.ComponentModel.DataAnnotations;

namespace CMSBlog.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ResData), ErrorMessageResourceName = "RegiterEmail")]
        [EmailAddress(ErrorMessageResourceType = typeof(ResData), ErrorMessageResourceName = "HasEmail")]

        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResData), ErrorMessageResourceName = "RegisterPassword")]
        public string Password { get; set; }
    }
}
