namespace GDrinks.Web.Controllers
{
    using GDrinks.Common;
    using GDrinks.Services;
    using GDrinks.Web.Models.Drinks;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class DrinksController : Controller
    {
        private readonly IDrinkService drinks;

        public DrinksController(IDrinkService drinks)
        {
            this.drinks = drinks;
        }

        public async Task<IActionResult> Index(int id = 1, string category = null)
        {
            var drinks = await this.drinks.AllAsync(id, category);

            var drinksCount = 
                category == null ? 
                await this.drinks.CountAsync() : 
                    (category.ToLower() == "alcoholic" ?
                        await this.drinks.AlcoholicCountAsync() :
                        await this.drinks.NonAlcoholicCountAsync());

            var model = new DrinksListingViewModel
            {
                Drinks = drinks,
                CurrentPage = id,
                DrinksCount = drinksCount,
                Category = category ?? string.Empty,
                PagesCount = (int)Math.Ceiling(drinksCount / (decimal)WebConstants.DrinksPerPage)
            };

            return View(model);
        }
    }
}