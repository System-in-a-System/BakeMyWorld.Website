using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BakeMyWorld.Website.Areas.API.Models
{
    public class CredentialsDto
    {
        [Required]
        [MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
