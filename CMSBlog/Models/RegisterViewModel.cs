using CMSBlog.Resources;
using System.ComponentModel.DataAnnotations;

namespace CMSBlog.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType =typeof(ResData),ErrorMessageResourceName = "RegisterName")]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType =typeof(ResData),ErrorMessageResourceName = "RegiterEmail")]
        [EmailAddress(ErrorMessageResourceType = typeof(ResData), ErrorMessageResourceName = "HasEmail")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType =typeof(ResData),ErrorMessageResourceName = "RegisterPassword")]
        public string Paassord { get; set; }
        [Required(ErrorMessageResourceType =typeof(ResData),ErrorMessageResourceName = "RegisterRePassword")]
        [Compare("Paassord",ErrorMessageResourceType =typeof(ResData),ErrorMessageResourceName = "RegisterPsswordEqual")]
        public string ConfirmPaassord { get; set; }
    }
}
