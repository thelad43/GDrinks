namespace GDrinks.Web.Controllers
{
    using GDrinks.Services;
    using GDrinks.Services.Models;
    using GDrinks.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart shoppingCart;
        private readonly IDrinkService drinks;

        public ShoppingCartController(ShoppingCart shoppingCart, IDrinkService drinks)
        {
            this.shoppingCart = shoppingCart;
            this.drinks = drinks;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await this.shoppingCart.GetCartItemsAsync();

            this.shoppingCart.Items = items;

            var model = new ShoppingCartViewModel
            {
                ShoppingCart = this.shoppingCart,
                ShoppingCartTotal = await this.shoppingCart.GetTotalAsync()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var drink = await this.drinks.ByIdAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            await this.shoppingCart.AddAsync(drink);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            var drink = await this.drinks.ByIdAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            await this.shoppingCart.RemoveAsync(drink);

            return RedirectToAction(nameof(Index));
        }
    }
}