namespace GDrinks.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DrinksController : BaseAdminController
    {
        [HttpGet]
        public IActionResult Add() => View();

        // TODO: Add post for add action
    }
}