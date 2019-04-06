namespace GDrinks.Services.Models
{
    using GDrinks.Common.Mapping;
    using GDrinks.Models;

    public class DrinkDetailsServiceModel : IMapFrom<Drink>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FullDescription { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public bool IsPreferredDrink { get; set; }

        public bool InStock { get; set; }

        public Category Category { get; set; }
    }
}