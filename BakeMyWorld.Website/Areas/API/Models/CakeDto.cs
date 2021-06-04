using BakeMyWorld.Website.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BakeMyWorld.Website.Areas.API.Models
{
    public class CakeDto
    {
      

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public Uri ImageUrl { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public int CorporateId { get; set; }
        public ICollection<Category> Categories { get; set; }
            = new List<Category>();

        public ICollection<Corporate> Corporates { get; set; }
         = new List<Corporate>();

  
    }
}
