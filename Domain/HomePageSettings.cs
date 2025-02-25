using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class HomePageSettings :SecurityBase
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string key { get; set; }
        [Required]
        [MaxLength(255)]
        public string value { get; set; }
        

    }
}
