﻿namespace GDrinks.Services.Implementations
{
    using GDrinks.Common;
    using GDrinks.Common.Mapping;
    using GDrinks.Data;
    using GDrinks.Models;
    using GDrinks.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DrinkService : IDrinkService
    {
        private readonly GDrinksDbContext db;

        public DrinkService(GDrinksDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<DrinkServiceModel>> AllAsync(int page, string categoryName, string search)
        {
            var drinks = this.db
               .Drinks
               .OrderBy(d => d.Name)
               .Skip((page - 1) * WebConstants.DrinksPerPage)
               .Take(WebConstants.DrinksPerPage)
               .AsQueryable();

            if (categoryName == null && search == null)
            {
                return await drinks
                    .To<DrinkServiceModel>()
                    .ToListAsync();
            }

            Category category = null;

            if (categoryName != null)
            {
                category = await this.db
                    .Categories
                    .FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
            }

            if (search != null && category == null)
            {
                return await drinks
                    .Where(d => d.Name.ToLower().Contains(search.ToLower()))
                    .To<DrinkServiceModel>()
                    .ToListAsync();
            }

            if (category == null)
            {
                throw new InvalidOperationException($"Category {categoryName} is not found.");
            }

            return await drinks
                .Where(d => d.Category == category)
                .To<DrinkServiceModel>()
                .ToListAsync();
        }

        public async Task<int> CountAsync()
            => await this.db
                .Drinks
                .CountAsync();

        public async Task<int> AlcoholicCountAsync()
           => await this.db
               .Drinks
               .Where(d => d.Category.Name.ToLower() != WebConstants.NonAlcoholic)
               .CountAsync();

        public async Task<int> NonAlcoholicCountAsync()
            => await this.db
                .Drinks
                .Where(d => d.Category.Name.ToLower() != WebConstants.Alcoholic)
                .CountAsync();

        public async Task<IEnumerable<DrinkServiceModel>> Preferred()
            => await this.db
                .Drinks
                .OrderBy(d => d.Name)
                .Where(d => d.IsPreferredDrink)
                .To<DrinkServiceModel>()
                .ToListAsync();

        public async Task<Drink> ByIdAsync(int id)
            => await this.db
                .Drinks
                .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<int> CountBySearchAsync(string search)
            => await this.db
                .Drinks
                .Where(d => d.Name.ToLower().Contains(search.ToLower()))
                .CountAsync();
    }
}