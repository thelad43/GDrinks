namespace GDrinks.Services.Implementations
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
               .AsQueryable();

            if (categoryName == null && search == null)
            {
                return await drinks
                    .Skip((page - 1) * WebConstants.DrinksPerPage)
                    .Take(WebConstants.DrinksPerPage)
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
                    .Skip((page - 1) * WebConstants.DrinksPerPage)
                    .Take(WebConstants.DrinksPerPage)
                    .To<DrinkServiceModel>()
                    .ToListAsync();
            }

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
                .Where(d => d.IsPreferred)
                .To<DrinkServiceModel>()
                .ToListAsync();

        public async Task<Drink> ByIdAsync(int id)
            => await this.db
                .Drinks
                .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<TModel> ByIdAsync<TModel>(int id)
            => await this.db
                .Drinks
                .Where(d => d.Id == id)
                .To<TModel>()
                .FirstOrDefaultAsync();

        public async Task<int> CountBySearchAsync(string search)
            => await this.db
                .Drinks
                .Where(d => d.Name.ToLower().Contains(search == null ? string.Empty : search.ToLower()))
                .CountAsync();

        public async Task AddAsync(
            string name,
            string description,
            string fullDescription,
            decimal price,
            string imageUrl,
            string imageThumbnailUrl,
            bool isPreferred,
            bool isInStock,
            int categoryId)
        {
            var drink = new Drink
            {
                Name = name,
                Description = description,
                FullDescription = fullDescription,
                Price = price,
                ImageUrl = imageUrl,
                ImageThumbnailUrl = imageThumbnailUrl,
                IsPreferred = isPreferred,
                IsInStock = isInStock,
                CategoryId = categoryId
            };

            await this.db.AddAsync(drink);
            await this.db.SaveChangesAsync();
        }

        public async Task EditAsync(
            int id,
            string name,
            string description,
            string fullDescription,
            decimal price,
            string imageUrl,
            string imageThumbnailUrl,
            bool isPreferred,
            bool isInStock,
            int categoryId)
        {
            var drink = await this.db.Drinks.FindAsync(id);

            drink.Name = name;
            drink.Description = description;
            drink.FullDescription = fullDescription;
            drink.Price = price;
            drink.ImageUrl = imageUrl;
            drink.ImageThumbnailUrl = imageThumbnailUrl;
            drink.IsPreferred = isPreferred;
            drink.IsInStock = isInStock;
            drink.CategoryId = categoryId;

            await this.db.SaveChangesAsync();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var drink = await this.ByIdAsync(id);
            this.db.Remove(drink);
            await this.db.SaveChangesAsync();

            return drink.Name;
        }
    }
}