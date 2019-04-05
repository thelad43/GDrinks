﻿namespace GDrinks.Services.Implementations
{
    using GDrinks.Data;
    using GDrinks.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly GDrinksDbContext db;

        public CategoryService(GDrinksDbContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<Category>> GetAsync()
            => await this.db
                .Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
    }
}