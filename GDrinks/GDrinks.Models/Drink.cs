using System.Collections.Generic;

namespace GDrinks.Models
{
    public class Drink
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FullDescription { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string ImageThumbnailUrl { get; set; }

        public bool IsPreferredDrink { get; set; }

        public bool InStock { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}