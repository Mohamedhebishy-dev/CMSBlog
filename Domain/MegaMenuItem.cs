using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MegaMenuItem
    {
        public int Id { get; set; } 
        public string Title { get; set; } 
        public string Url { get; set; } 
        public string? Icon { get; set; } 
        public List<MegaMenuItem> SubItems { get; set; } = new List<MegaMenuItem>(); 
    }
}
