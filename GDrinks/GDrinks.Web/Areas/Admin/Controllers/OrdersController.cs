namespace GDrinks.Web.Areas.Admin.Controllers
{
    using GDrinks.Common;
    using GDrinks.Services;
    using GDrinks.Web.Areas.Admin.Models.Orders;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class OrdersController : BaseAdminController
    {
        private readonly IOrderService orders;

        public OrdersController(IOrderService orders)
        {
            this.orders = orders;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id = 1)
        {
            var orders = await this.orders.AllAsync(id);

            var ordersCount = await this.orders.CountAsync();

            var model = new OrdersListingViewModel
            {
                Orders = orders,
                CurrentPage = id,
                OrdersCount = ordersCount,
                PagesCount = (int)Math.Ceiling(ordersCount / (decimal)WebConstants.OrdersPerPage)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Items(int id)
            => View(await this.orders.ItemsByOrderIdAsync(id));
    }
}