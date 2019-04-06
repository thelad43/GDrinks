namespace GDrinks.Services.Implementations
{
    using GDrinks.Data;
    using GDrinks.Models;
    using GDrinks.Services.Models;
    using System;
    using System.Threading.Tasks;

    public class OrderService : IOrderService
    {
        private readonly GDrinksDbContext db;

        public OrderService(GDrinksDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string addressLine, string zipCode, string country, string userId, ShoppingCart shoppingCart)
        {
            var order = new Order
            {
                AddressLine = addressLine,
                ZipCode = zipCode,
                Country = country,
                OrderedOn = DateTime.UtcNow,
                UserId = userId
            };

            await this.db.AddAsync(order);
            await this.db.SaveChangesAsync();

            var items = shoppingCart.Items;

            foreach (var item in items)
            {
                var orderDetail = new OrderItem()
                {
                    Amount = item.Amount,
                    DrinkId = item.Drink.Id,
                    OrderId = order.Id,
                    Price = item.Drink.Price
                };

                await this.db.AddAsync(orderDetail);
            }

            await db.SaveChangesAsync();
        }
    }
}