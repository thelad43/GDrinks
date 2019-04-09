namespace GDrinks.Web.Areas.Admin.Controllers
{
    using GDrinks.Services;
    using GDrinks.Web.Areas.Admin.Models.Drinks;
    using GDrinks.Web.Controllers;
    using GDrinks.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class DrinksController : BaseAdminController
    {
        private readonly IDrinkService drinks;
        private readonly ICategoryService categories;

        public DrinksController(IDrinkService drinks, ICategoryService categories)
        {
            this.drinks = drinks;
            this.categories = categories;
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(DrinkFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = await this.categories.ByNameAsync(model.Category);

            if (category == null)
            {
                TempData.AddErrorMessage($"Invalid category: {model.Category}");
                return View(model);
            }

            await this.drinks.AddAsync(
                model.Name,
                model.Description,
                model.FullDescription,
                model.Price,
                model.ImageUrl,
                model.ImageThumbnailUrl,
                model.IsPreferredDrink,
                model.InStock,
                category.Id);

            TempData.AddSuccessMessage($"Successfully created new drink: {model.Name}");

            return this.RedirectToCustomAction(nameof(HomeController.Index), nameof(HomeController));
        }
    }
}