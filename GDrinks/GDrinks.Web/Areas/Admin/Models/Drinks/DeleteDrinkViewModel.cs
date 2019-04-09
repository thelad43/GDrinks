namespace GDrinks.Web.Areas.Admin.Models.Drinks
{
    using GDrinks.Common.Mapping;
    using GDrinks.Models;

    public class DeleteDrinkViewModel : IMapFrom<Drink>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}