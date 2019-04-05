namespace GDrinks.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Drink> Drinks { get; set; } = new List<Drink>();
    }
}