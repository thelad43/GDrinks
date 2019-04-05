namespace GDrinks.Web.Models.Drinks
{
    using GDrinks.Services.Models;
    using System.Collections.Generic;

    public class DrinksListingViewModel
    {
        public IEnumerable<DrinkServiceModel> Drinks { get; set; } = new List<DrinkServiceModel>();

        public string Category { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int DrinksCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;
    }
}