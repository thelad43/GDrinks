﻿namespace GDrinks.Web.Controllers
{
    using GDrinks.Common;
    using GDrinks.Services;
    using GDrinks.Services.Models;
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

        [HttpGet]
        public async Task<IActionResult> Index(int id = 1, string category = null, string search = null)
        {
            var drinks = await this.drinks.AllAsync(id, category, search);

            var searchedDrinks = await this.drinks.CountBySearchAsync(search);

            var drinksCount =
                category == null ?
                await this.drinks.CountAsync() :
                    (category.ToLower() == "alcoholic" ?
                        await this.drinks.AlcoholicCountAsync() :
                        await this.drinks.NonAlcoholicCountAsync());

            if (search != null)
            {
                drinksCount -= drinksCount - searchedDrinks;
            }

            var model = new DrinksListingViewModel
            {
                Drinks = drinks,
                CurrentPage = id,
                DrinksCount = drinksCount,
                Category = category ?? string.Empty,
                Search = search,
                PagesCount = (int)Math.Ceiling(drinksCount / (decimal)WebConstants.DrinksPerPage)
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var drink = await this.drinks.ByIdAsync<DrinkDetailsServiceModel>(id);

            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }
    }
}