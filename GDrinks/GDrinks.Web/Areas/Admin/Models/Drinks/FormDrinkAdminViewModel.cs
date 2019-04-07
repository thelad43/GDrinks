namespace GDrinks.Web.Areas.Admin.Models.Drinks
{
    using GDrinks.Models;

    public class FormDrinkAdminViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string FullDescription { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string ImageThumbnailUrl { get; set; }

        public bool IsPreferredDrink { get; set; }

        public bool InStock { get; set; }

        public Category Category { get; set; }
    }
}