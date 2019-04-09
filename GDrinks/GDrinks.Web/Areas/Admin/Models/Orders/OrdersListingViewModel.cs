namespace GDrinks.Web.Areas.Admin.Models.Orders
{
    using GDrinks.Services.Models;
    using System.Collections.Generic;

    public class OrdersListingViewModel
    {
        public IEnumerable<OrderServiceModel> Orders { get; set; } = new List<OrderServiceModel>();

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int OrdersCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;
    }
}