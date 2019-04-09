namespace GDrinks.Web.Components
{
    using GDrinks.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryService categories;

        public CategoryMenu(ICategoryService categories)
        {
            this.categories = categories;
        }

        public async Task<IViewComponentResult> InvokeAsync()
            => View(await this.categories.AllAsync());
    }
}