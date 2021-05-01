using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeMyWorld.ConsoleManager
{
    class Cake
    {
        public Cake()
        {

        }

        public Cake(string name, string description, string imageUrl, decimal price, int categoryId)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            CategoryId = categoryId;
        }

        public Cake(int id, string name, string description, string imageUrl, decimal price, int categoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            CategoryId = categoryId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
