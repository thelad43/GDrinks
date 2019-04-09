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

    public class OrderService : IOrderService
    {
        private readonly GDrinksDbContext db;

        public OrderService(GDrinksDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<OrderServiceModel>> AllAsync(int page)
            => await this.db
                .Orders
                .OrderBy(o => o.OrderedOn)
                .Skip((page - 1) * WebConstants.OrdersPerPage)
                .Take(WebConstants.OrdersPerPage)
                .To<OrderServiceModel>()
                .ToListAsync();

        public async Task<int> CountAsync()
            => await this.db
                .Orders
                .CountAsync();

        public async Task CreateAsync(
            string addressLine,
            string zipCode,
            string country,
            string userId,
            ShoppingCart shoppingCart)
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

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderItemServiceModel>> ItemsByOrderIdAsync(int id)
            => await this.db
                .OrderItems
                .Where(oi => oi.OrderId == id)
                .OrderBy(oi => oi.Amount)
                .To<OrderItemServiceModel>()
                .ToListAsync();
    }
}