namespace GDrinks.Services
{
    using GDrinks.Models;
    using GDrinks.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDrinkService
    {
        Task<IEnumerable<DrinkServiceModel>> AllAsync(int page, string categoryName, string search);

        Task<IEnumerable<DrinkServiceModel>> PreferredAsync();

        Task<int> CountAsync();

        Task<int> AlcoholicCountAsync();

        Task<int> NonAlcoholicCountAsync();

        Task<Drink> ByIdAsync(int id);

        Task<TModel> ByIdAsync<TModel>(int id);

        Task<int> CountBySearchAsync(string search);

        Task AddAsync(
            string name,
            string description,
            string fullDescription,
            decimal price,
            string imageUrl,
            string imageThumbnailUrl,
            bool isPreferred,
            bool isInStock,
            int categoryId);

        Task EditAsync(
            int id,
            string name,
            string description,
            string fullDescription,
            decimal price,
            string imageUrl,
            string imageThumbnailUrl,
            bool isPreferred,
            bool isInStock,
            int categoryId);

        Task<string> DeleteAsync(int id);
    }
}