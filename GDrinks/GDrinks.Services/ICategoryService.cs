namespace GDrinks.Services
{
    using GDrinks.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IEnumerable<Category>> AllAsync();

        Task<Category> ByNameAsync(string categoryName);

        Task<Category> ByIdAsync(int categoryId);
    }
}