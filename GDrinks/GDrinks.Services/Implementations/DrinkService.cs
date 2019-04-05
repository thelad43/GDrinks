namespace GDrinks.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GDrinks.Common;
    using GDrinks.Common.Mapping;
    using GDrinks.Data;
    using GDrinks.Services.Models;
    using Microsoft.EntityFrameworkCore;

    public class DrinkService : IDrinkService
    {
        private readonly GDrinksDbContext db;

        public DrinkService(GDrinksDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<DrinkServiceModel>> AllAsync(int page, string categoryName)
        {
            var drinks = this.db
               .Drinks
               .OrderBy(d => d.Name)
               .AsQueryable();

            if (categoryName == null)
            {
                return await drinks
                    .Skip((page - 1) * WebConstants.DrinksPerPage)
                    .Take(WebConstants.DrinksPerPage)
                    .To<DrinkServiceModel>()
                    .ToListAsync();
            }

            var category = await this.db
                .Categories
                .FirstOrDefaultAsync(c => c.Name == categoryName);

            if (category == null)
            {
                throw new InvalidOperationException($"Category {categoryName} is not found.");
            }

            return await drinks
                .Where(d => d.Category == category)
                .Skip((page - 1) * WebConstants.DrinksPerPage)
                .Take(WebConstants.DrinksPerPage)
                .To<DrinkServiceModel>()
                .ToListAsync();
        }

        public async Task<int> CountAsync()
            => await this.db
                .Drinks
                .CountAsync();

        public async Task<IEnumerable<DrinkServiceModel>> Preferred()
            => await this.db
                .Drinks
                .OrderBy(d => d.Name)
                .Where(d => d.IsPreferredDrink)
                .To<DrinkServiceModel>()
                .ToListAsync();
    }
}