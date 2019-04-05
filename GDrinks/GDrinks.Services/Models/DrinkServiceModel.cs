namespace GDrinks.Services.Models
{
    using GDrinks.Common.Mapping;
    using GDrinks.Models;

    public class DrinkServiceModel : IMapFrom<Drink>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageThumbnailUrl { get; set; }
    }
}