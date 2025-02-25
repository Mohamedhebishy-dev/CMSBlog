using Domain;

namespace CMSBlog.Models
{
    public class PrimaryViewModel
    {

        public Guid Id { get; set; }
        public string? WebSiteName { get; set; }
        public string? Description { get; set; }
        public string? LogoName { get; set; }
        public string? Adress { get; set; }
        public string? EmailAddress { get; set; }
        public string? ContactNumber { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? XLink { get; set; }
        public string? LinkedInLink { get; set; }
        public string? ColumnLinks1 { get; set; }
        public string? ColumnLinks2 { get; set; }
        public List<MegaMenuItem> Menu { get; set; }


    }
}
