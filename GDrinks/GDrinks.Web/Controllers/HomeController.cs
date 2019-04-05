namespace GDrinks.Web.Controllers
{
    using GDrinks.Services;
    using GDrinks.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IDrinkService drinks;

        public HomeController(IDrinkService drinks)
        {
            this.drinks = drinks;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await this.drinks.Preferred());

        [HttpGet]
        public IActionResult Privacy() => View();

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}