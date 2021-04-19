using Highscores.Website.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeMyWorld.Website.Data.Entities
{
    public class GiftBox
    {
        public GiftBox(string name, string description, Uri imageUrl, decimal price)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            UrlSlug = name.Slugify();
        }

        public GiftBox(int id, string name, string description, Uri imageUrl, decimal price)
            : this(name, description, imageUrl, price)
        {
            Id = id;
        }

        public GiftBox(string name, string description, Uri imageUrl, decimal price, string urlSlug)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            UrlSlug = urlSlug;
        }

        public GiftBox(int id, string name, string description, Uri imageUrl, decimal price, string urlSlug)
            : this(name, description, imageUrl, price, urlSlug)
        {
            Id = id;
        }

        public int Id { get; protected set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public Uri ImageUrl { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(50)]
        public string UrlSlug { get; protected set; }

        public ICollection<Corporate> Corporates { get; set; }
            = new List<Corporate>();
    }
}

