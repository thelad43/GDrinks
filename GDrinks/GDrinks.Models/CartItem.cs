namespace GDrinks.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int DrinkId { get; set; }

        public Drink Drink { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}