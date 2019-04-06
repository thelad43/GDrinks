namespace GDrinks.Services
{
    using GDrinks.Services.Models;
    using System.Threading.Tasks;

    public interface IOrderService
    {
        Task CreateAsync(string addressLine, string zipCode, string country, string userId, ShoppingCart shoppingCart);
    }
}