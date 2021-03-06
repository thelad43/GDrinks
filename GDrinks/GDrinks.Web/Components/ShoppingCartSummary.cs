﻿namespace GDrinks.Web.Components
{
    using GDrinks.Services.Models;
    using GDrinks.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await this.shoppingCart.GetCartItemsAsync();

            this.shoppingCart.Items = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = this.shoppingCart,
                ShoppingCartTotal = await this.shoppingCart.GetTotalAsync()
            };

            return View(shoppingCartViewModel);
        }
    }
}