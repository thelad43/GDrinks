namespace GDrinks.Web.Controllers
{
    using GDrinks.Services;
    using GDrinks.Services.Models;
    using GDrinks.Web.Infrastructure.Extensions;
    using GDrinks.Web.Models.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ShoppingCart shoppingCart;
        private readonly IOrderService orders;

        public OrdersController(ShoppingCart shoppingCart, IOrderService orders)
        {
            this.shoppingCart = shoppingCart;
            this.orders = orders;
        }

        public async Task<IActionResult> Checkout()
        {
            var items = await this.shoppingCart.GetCartItemsAsync();

            this.shoppingCart.Items = items;

            if (!this.shoppingCart.Items.Any())
            {
                TempData.AddErrorMessage("The shopping cart is empty. Please add items before checkout.");
                return this.RedirectToCustomAction(nameof(HomeController.Index), nameof(HomeController));
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(OrderFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var items = await this.shoppingCart.GetCartItemsAsync();

            this.shoppingCart.Items = items;

            if (!this.shoppingCart.Items.Any())
            {
                TempData.AddErrorMessage("The shopping cart is empty. Please add items before checkout.");
                return this.RedirectToCustomAction(nameof(HomeController.Index), nameof(HomeController));
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.orders
                .CreateAsync(model.AddressLine, model.ZipCode, model.Country, userId, this.shoppingCart);

            TempData.AddSuccessMessage("Successfully ordered drinks! Thanks for your order!");

            await this.shoppingCart.ClearAsync();

            return this.RedirectToCustomAction(nameof(HomeController.Index), nameof(HomeController));
        }
    }
}