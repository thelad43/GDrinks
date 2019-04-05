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

    public class ShoppingCart
    {
        private readonly GDrinksDbContext db;

        public ShoppingCart(GDrinksDbContext db)
        {
            this.db = db;
        }

        public string Id { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public static ShoppingCart GetCart(IServiceProvider serviceProvider)
        {
            var session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var db = serviceProvider.GetService<GDrinksDbContext>();

            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(db) { Id = cartId };
        }

        public void AddToCart(Drink drink, int amount)
        {
            var shoppingCartItem =
                    this.db.CartItems.SingleOrDefault(
                        s => s.Drink.Id == drink.Id && s.ShoppingCartId == this.Id);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new CartItem
                {
                    ShoppingCartId = Id,
                    Drink = drink,
                    Amount = 1
                };

                this.db.CartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            this.db.SaveChanges();
        }

        public int RemoveFromCart(Drink drink)
        {
            var shoppingCartItem =
                    this.db.CartItems.SingleOrDefault(
                        s => s.Drink.Id == drink.Id && s.ShoppingCartId == this.Id);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    this.db.CartItems.Remove(shoppingCartItem);
                }
            }

            this.db.SaveChanges();

            return localAmount;
        }

        public List<CartItem> GetShoppingCartItems()
        {
            return this.Items ??
                    (this.Items = this.db.CartItems.Where(c => c.ShoppingCartId == this.Id)
                    .Include(s => s.Drink)
                    .ToList());
        }

        public void ClearCart()
        {
            var cartItems = this.db
                .CartItems
                .Where(cart => cart.ShoppingCartId == this.Id);

            this.db.CartItems.RemoveRange(cartItems);

            this.db.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
            => this.db
                .CartItems
                .Where(c => c.ShoppingCartId == this.Id)
                .Select(c => c.Drink.Price * c.Amount)
                .Sum();
    }
}