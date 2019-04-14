namespace GDrinks.Tests.Services
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using GDrinks.Data;
    using GDrinks.Models;
    using GDrinks.Services.Implementations;
    using Xunit;

    public class CategoryServiceTests
    {
        public CategoryServiceTests()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllAsyncShouldReturnAllCategoriesOrderedByName()
        {
            var db = DbInfrastructure.GetDatabase();

            const int Categories = 10;

            await SeedCategories(db, Categories);

            var categoryService = new CategoryService(db);

            var categories = await categoryService.AllAsync();

            categories
                .Should()
                .HaveCount(Categories);

            categories
                .Should()
                .BeInAscendingOrder(c => c.Name);
        }

        [Fact]
        public async Task ByNameAsyncShouldReturnCategoryByName()
        {
            var db = DbInfrastructure.GetDatabase();

            const int Categories = 10;

            await SeedCategories(db, Categories);

            var categoryService = new CategoryService(db);

            const string Name = "Name 7";

            var category = await categoryService.ByNameAsync(Name);

            category
                .Should()
                .NotBeNull();
        }

        [Fact]
        public async Task ByIdAsyncShouldReturnCategoryById()
        {
            var db = DbInfrastructure.GetDatabase();

            const int Categories = 10;

            await SeedCategories(db, Categories);

            var categoryService = new CategoryService(db);

            const int Id = 17;

            var category = await categoryService.ByIdAsync(Id);

            category
                .Should()
                .NotBeNull();
        }

        private static async Task SeedCategories(GDrinksDbContext db, int categories)
        {
            for (var i = 0; i < categories; i++)
            {
                await db.AddAsync(new Category
                {
                    Name = $"Name {i}"
                });
            }

            await db.SaveChangesAsync();
        }
    }
}