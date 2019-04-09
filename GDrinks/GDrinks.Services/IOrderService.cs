namespace GDrinks.Services
{
    using GDrinks.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderService
    {
        Task<IEnumerable<OrderServiceModel>> AllAsync(int page);

        Task CreateAsync(
            string addressLine,
            string zipCode,
            string country,
            string userId,
            ShoppingCart shoppingCart);

        Task<int> CountAsync();

        Task<IEnumerable<OrderItemServiceModel>> ItemsByOrderIdAsync(int id);
    }
}