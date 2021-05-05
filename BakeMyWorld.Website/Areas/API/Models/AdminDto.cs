using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BakeMyWorld.Website.Areas.API.Models
{
    public class AdminDto
    {
        [Required]
        [MaxLength(50)]
        public string AdminName { get; set; }

        [Required]
        [MaxLength(50)]
        public string AdminPassword { get; set; }
    }
}
