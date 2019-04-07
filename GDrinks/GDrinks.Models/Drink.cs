namespace GDrinks.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Drink
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string Description { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(200)]
        public string FullDescription { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Url]
        public string ImageThumbnailUrl { get; set; }

        public bool IsPreferredDrink { get; set; }

        public bool InStock { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}