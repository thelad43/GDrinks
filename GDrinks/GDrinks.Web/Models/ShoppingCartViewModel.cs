namespace GDrinks.Web.Models
{
    using GDrinks.Common.Mapping;
    using GDrinks.Services.Models;

    public class ShoppingCartViewModel : IMapFrom<ShoppingCart>
    {
        public ShoppingCart ShoppingCart { get; set; }

        public decimal ShoppingCartTotal { get; set; }
    }
}