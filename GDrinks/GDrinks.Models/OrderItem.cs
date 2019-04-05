﻿namespace GDrinks.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public int DrinkId { get; set; }

        public Drink Drink { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}