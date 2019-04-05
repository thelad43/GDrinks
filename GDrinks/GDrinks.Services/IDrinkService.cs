namespace GDrinks.Services
{
    using GDrinks.Models;
    using GDrinks.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDrinkService
    {
        Task<IEnumerable<DrinkServiceModel>> AllAsync(int page, string categoryName);

        Task<IEnumerable<DrinkServiceModel>> Preferred();

        Task<int> CountAsync();

        Task<int> AlcoholicCountAsync();

        Task<int> NonAlcoholicCountAsync();

        Task<Drink> ByIdAsync(int id);
    }
}