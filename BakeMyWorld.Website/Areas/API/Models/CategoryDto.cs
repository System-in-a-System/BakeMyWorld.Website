using BakeMyWorld.Website.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BakeMyWorld.Website.Areas.API.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public Uri ImageUrl { get; set; }

        public ICollection<Cake> Cakes { get; set; }
            = new List<Cake>();
    }
}
