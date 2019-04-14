namespace GDrinks.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static GDrinks.Common.DataConstants;
    using static GDrinks.Common.DataErrorMessages;

    public class Drink
    {
        public int Id { get; set; }

        [Required(ErrorMessage = DrinkNameErrorMessage)]
        [MinLength(DrinkNameMinLength, ErrorMessage = DrinkNameMinLengthErrorMessage)]
        [MaxLength(DrinkNameMaxLength, ErrorMessage = DrinkNameMaxLengthErrorMessage)]
        public string Name { get; set; }

        [Required(ErrorMessage = DrinkDescriptionErrorMessage)]
        [MinLength(DrinkDescriptionMinLength, ErrorMessage = DrinkDescriptionMinLengthErrorMessage)]
        [MaxLength(DrinkDescriptionMaxLength, ErrorMessage = DrinkDescriptionMaxLengthErrorMessage)]
        public string Description { get; set; }

        [Required(ErrorMessage = DrinkFullDescriptionErrorMessage)]
        [MinLength(DrinkFullDescriptionMinLength, ErrorMessage = DrinkFullDescriptionMinLengthErrorMessage)]
        [MaxLength(DrinkFullDescriptionMaxLength, ErrorMessage = DrinkFullDescriptionMaxLengthErrorMessage)]
        public string FullDescription { get; set; }

        [Range(DrinkPriceMinLength, DrinkPriceMaxLength, ErrorMessage = DrinkPriceErrorMessage)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = DrinkImageUrlErrorMessage)]
        [Url]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = DrinkImageThumbnailUrlErrorMessage)]
        [Url]
        public string ImageThumbnailUrl { get; set; }

        public bool IsPreferred { get; set; }

        public bool IsInStock { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}