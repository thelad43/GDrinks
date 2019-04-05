namespace GDrinks.Web.Models
{
    using GDrinks.Services.Models;

    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }

        public decimal ShoppingCartTotal { get; set; }
    }
}