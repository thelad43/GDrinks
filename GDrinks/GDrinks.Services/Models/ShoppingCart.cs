namespace GDrinks.Services.Models
{
    using GDrinks.Data;
    using GDrinks.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCart
    {
        private readonly GDrinksDbContext db;

        public ShoppingCart(GDrinksDbContext db)
        {
            this.db = db;
        }

        public string Id { get; private set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public static ShoppingCart Get(IServiceProvider serviceProvider)
        {
            var session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var db = serviceProvider.GetService<GDrinksDbContext>();

            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(db) { Id = cartId };
        }

        public async Task AddAsync(Drink drink)
        {
            var cartItem = await this.db
                .CartItems
                .SingleOrDefaultAsync(s => s.Drink.Id == drink.Id && s.ShoppingCartId == this.Id);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ShoppingCartId = Id,
                    Drink = drink,
                    Amount = 1
                };

                await this.db.CartItems.AddAsync(cartItem);
            }
            else
            {
                cartItem.Amount++;
            }

            await this.db.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(Drink drink)
        {
            var cartItem = await this.db
                .CartItems
                .SingleOrDefaultAsync(s => s.Drink.Id == drink.Id && s.ShoppingCartId == this.Id);

            var localAmount = 0;

            if (cartItem != null)
            {
                if (cartItem.Amount > 1)
                {
                    cartItem.Amount--;
                    localAmount = cartItem.Amount;
                }
                else
                {
                    this.db.CartItems.Remove(cartItem);
                }
            }

            await this.db.SaveChangesAsync();

            return localAmount;
        }

        public async Task<List<CartItem>> GetCartItemsAsync()
            => await this.db
                .CartItems
                .Where(ci => ci.ShoppingCartId == this.Id)
                .Include(ci => ci.Drink)
                .ToListAsync();

        public async Task ClearAsync()
        {
            var cartItems = this.db
                .CartItems
                .Where(cart => cart.ShoppingCartId == this.Id);

            this.db.CartItems.RemoveRange(cartItems);

            await this.db.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalAsync()
            => await this.db
                .CartItems
                .Where(c => c.ShoppingCartId == this.Id)
                .Select(c => c.Drink.Price * c.Amount)
                .SumAsync();
    }
}