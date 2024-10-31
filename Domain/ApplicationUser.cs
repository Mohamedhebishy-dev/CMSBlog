﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ApplicationUser :IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [AllowNull]
        public string? ImageUser { get; set; }

        public bool Blocked { get; set; }
    }
}
