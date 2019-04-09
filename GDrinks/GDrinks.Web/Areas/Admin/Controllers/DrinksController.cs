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
                model.IsPreferred,
                model.IsInStock,
                category.Id);

            TempData.AddSuccessMessage($"Successfully created new drink: {model.Name}");

            return this.RedirectToCustomAction(nameof(HomeController.Index), nameof(HomeController));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var drink = await this.drinks.ByIdAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            var category = await this.categories.ByIdAsync(drink.CategoryId);

            var model = new EditDrinkViewModel
            {
                Id = id,
                Name = drink.Name,
                Description = drink.Description,
                FullDescription = drink.FullDescription,
                Price = drink.Price,
                ImageUrl = drink.ImageUrl,
                ImageThumbnailUrl = drink.ImageThumbnailUrl,
                IsPreferred = drink.IsPreferred,
                IsInStock = drink.IsInStock,
                Category = category.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditDrinkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var drink = await this.drinks.ByIdAsync(model.Id);

            if (drink == null)
            {
                return NotFound();
            }

            var category = await this.categories.ByNameAsync(model.Category);

            if (category == null)
            {
                TempData.AddErrorMessage($"Invalid category: {model.Category}");
                return View(model);
            }

            await this.drinks.EditAsync(
                model.Id,
                model.Name,
                model.Description,
                model.FullDescription,
                model.Price,
                model.ImageUrl,
                model.ImageThumbnailUrl,
                model.IsPreferred,
                model.IsInStock,
                category.Id);

            TempData.AddSuccessMessage($"Successfully edited drink: {model.Name}");

            return this.RedirectToCustomAction(nameof(HomeController.Index), nameof(HomeController));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var drink = await this.drinks.ByIdAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            var model = new DeleteDrinkViewModel
            {
                Id = id,
                Name = drink.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Destroy(int id)
        {
            var drink = await this.drinks.ByIdAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            var drinkName = await this.drinks.DeleteAsync(id);

            TempData.AddSuccessMessage($"Successfully deleted drink: {drinkName}");

            return this.RedirectToCustomAction(nameof(HomeController.Index), nameof(HomeController));
        }
    }
}