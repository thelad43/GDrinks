﻿namespace GDrinks.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Drink
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(2000)]
        public string FullDescription { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Url]
        public string ImageThumbnailUrl { get; set; }

        public bool IsPreferred { get; set; }

        public bool IsInStock { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}