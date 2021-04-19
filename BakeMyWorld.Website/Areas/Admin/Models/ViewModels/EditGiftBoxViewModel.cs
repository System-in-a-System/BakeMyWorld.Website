using BakeMyWorld.Website.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeMyWorld.Website.Areas.Admin.Models.ViewModels
{
    public class EditGiftBoxViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Image URL")]
        public Uri ImageUrl { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public ICollection<Corporate> Corporates { get; set; }
    }
}
