namespace GDrinks.Tests.Services
{
    using FluentAssertions;
    using GDrinks.Common;
    using GDrinks.Data;
    using GDrinks.Models;
    using GDrinks.Services.Implementations;
    using GDrinks.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class DrinkServiceTests
    {
        private readonly Random random;

        private const string Name = "Beer";
        private const string Description = "Alcoholic drink";
        private const string FullDescription = "Full Description";
        private const decimal Price = 2.5656M;
        private const string ImageUrl = "some image url";
        private const string ImageThumbnailUrl = "some image thumbnail";
        private const int CategoryId = 3;

        public DrinkServiceTests()
        {
            Tests.Initialize();
            this.random = new Random();
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectDrinksByPageWithoutCategoryAndSearch()
        {
            var db = DbInfrastructure.GetDatabase();

            // 90 drinks with 5 categories
            await this.SeedData(db, 90, 5);

            var drinkService = new DrinkService(db);

            for (var i = 0; i < 10; i++)
            {
                var drinks = await drinkService.AllAsync(i + 1, null, null);

                drinks
                    .Should()
                    .HaveCount(WebConstants.DrinksPerPage);

                drinks
                    .Should()
                    .BeInAscendingOrder(d => d.Name);
            }
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectDrinksByPageWithoutSearch()
        {
            var db = DbInfrastructure.GetDatabase();

            var category = new Category
            {
                Name = "Some Category"
            };

            await db.AddAsync(category);
            await db.SaveChangesAsync();

            await this.SeedDataByCategory(db, 180, category.Id);

            var drinkService = new DrinkService(db);

            for (var i = 0; i < 20; i++)
            {
                var drinks = await drinkService.AllAsync(i + 1, category.Name, null);

                drinks
                    .Should()
                    .HaveCount(WebConstants.DrinksPerPage);

                drinks
                    .Should()
                    .BeInAscendingOrder(d => d.Name);

                foreach (var drink in drinks)
                {
                    var drinkFromDb = await db
                        .Drinks
                        .FindAsync(drink.Id);

                    drinkFromDb.CategoryId.Should().Be(category.Id);
                }
            }
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectDrinksByPageWithoutCategory()
        {
            var db = DbInfrastructure.GetDatabase();

            // 270 drinks with 1 categories
            await this.SeedData(db, 270, 1);

            var drinkService = new DrinkService(db);

            const string Search = "0";

            for (var i = 0; i < 3; i++)
            {
                var drinks = await drinkService.AllAsync(i + 1, null, Search);

                drinks
                    .Should()
                    .HaveCount(WebConstants.DrinksPerPage);

                drinks
                    .Should()
                    .BeInAscendingOrder(d => d.Name);

                foreach (var drink in drinks)
                {
                    drink
                        .Name
                        .ToLower()
                        .Should()
                        .Contain(Search.ToLower());
                }
            }
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectDrinksByPageWithCategoryAndSearch()
        {
            var db = DbInfrastructure.GetDatabase();

            var categories = new List<Category>();

            for (var i = 0; i < 10; i++)
            {
                var newCategory = new Category
                {
                    Name = $"Some Category {i}"
                };

                await db.AddAsync(newCategory);
                categories.Add(newCategory);
            }

            for (var i = 0; i < 270; i++)
            {
                await db.AddAsync(new Drink
                {
                    Name = $"Some Drink",
                    CategoryId = categories[this.random.Next(0, categories.Count)].Id,
                });
            }

            await db.SaveChangesAsync();

            var category = new Category
            {
                Name = "Custom category"
            };

            await db.AddAsync(category);
            await db.SaveChangesAsync();

            await this.SeedDataByCategory(db, 504, category.Id);

            var drinkService = new DrinkService(db);

            const string Search = "0";

            for (var i = 0; i < 10; i++)
            {
                var drinks = await drinkService.AllAsync(i + 1, category.Name, Search);

                drinks
                    .Should()
                    .HaveCount(WebConstants.DrinksPerPage);

                drinks
                    .Should()
                    .BeInAscendingOrder(d => d.Name);

                foreach (var drink in drinks)
                {
                    var drinkFromDb = await db
                        .Drinks
                        .FindAsync(drink.Id);

                    drinkFromDb
                        .Name
                        .ToLower()
                        .Should()
                        .Contain(Search.ToLower());

                    drinkFromDb
                        .CategoryId
                        .Should()
                        .Be(category.Id);
                }
            }
        }

        [Fact]
        public void AllAsyncShouldThrowExceptionIfCategoryIsNotFound()
        {
            var db = DbInfrastructure.GetDatabase();

            var drinkService = new DrinkService(db);

            Func<Task> func = async () => await drinkService.AllAsync(1, "Invalid CateGorY", null);

            func
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Category Invalid CateGorY is not found.");
        }

        [Fact]
        public async Task PreferredAsyncShoulReturnOnlyPrefferedDrinks()
        {
            var db = DbInfrastructure.GetDatabase();

            var drinkService = new DrinkService(db);

            const int Drinks = 90;

            // 90 preffered drinks with 3 categories
            await this.SeedData(db, Drinks, 3, true);

            var drinks = await drinkService.PreferredAsync();

            drinks
                .Should()
                .HaveCount(Drinks);

            drinks
                .Should()
                .BeInAscendingOrder(d => d.Name);

            foreach (var drink in drinks)
            {
                var drinkFromDb = await db
                    .Drinks
                    .FindAsync(drink.Id);

                drinkFromDb.IsPreferred.Should().BeTrue();
            }
        }

        [Fact]
        public async Task CountAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            const int Drinks = 150;

            await this.SeedData(db, Drinks, 5);

            var drinkService = new DrinkService(db);

            var count = await drinkService.CountAsync();

            count.Should().Be(Drinks);
        }

        [Fact]
        public async Task AlcoholicCountAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            const int AlcoholicDrinks = 150;

            var alcoholicCategory = new Category
            {
                Name = "Alcoholic"
            };

            var nonAlcoholicCategory = new Category
            {
                Name = "Non-Alcoholic"
            };

            await db.AddRangeAsync(alcoholicCategory, nonAlcoholicCategory);
            await db.SaveChangesAsync();

            await this.SeedDataByCategory(db, AlcoholicDrinks, alcoholicCategory.Id);
            await this.SeedDataByCategory(db, 1234, nonAlcoholicCategory.Id);

            var drinkService = new DrinkService(db);

            var count = await drinkService.AlcoholicCountAsync();

            count.Should().Be(AlcoholicDrinks);
        }

        [Fact]
        public async Task NonAlcoholicCountAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            const int NonAlcoholicDrinks = 150;

            var alcoholicCategory = new Category
            {
                Name = "Alcoholic"
            };

            var nonAlcoholicCategory = new Category
            {
                Name = "Non-Alcoholic"
            };

            await db.AddRangeAsync(alcoholicCategory, nonAlcoholicCategory);
            await db.SaveChangesAsync();

            await this.SeedDataByCategory(db, 1234, alcoholicCategory.Id);
            await this.SeedDataByCategory(db, NonAlcoholicDrinks, nonAlcoholicCategory.Id);

            var drinkService = new DrinkService(db);

            var count = await drinkService.NonAlcoholicCountAsync();

            count.Should().Be(NonAlcoholicDrinks);
        }

        [Fact]
        public async Task ByIdAsyncShouldReturnDrinkById()
        {
            var db = DbInfrastructure.GetDatabase();

            var drink = new Drink
            {
                Name = "Drink"
            };

            await db.AddAsync(drink);

            // 1000 drinks with 10 categories
            await this.SeedData(db, 1000, 10);

            var drinkService = new DrinkService(db);

            var actualDrink = await drinkService.ByIdAsync(drink.Id);

            actualDrink.Should().BeEquivalentTo(drink);
        }

        [Fact]
        public async Task ByIdAsyncTModelShouldReturnDrinkById()
        {
            var db = DbInfrastructure.GetDatabase();

            var drink = new Drink
            {
                Name = "Drink"
            };

            await db.AddAsync(drink);

            // 1000 drinks with 10 categories
            await this.SeedData(db, 1000, 10);

            var drinkService = new DrinkService(db);

            var actualDrink = await drinkService.ByIdAsync<DrinkServiceModel>(drink.Id);

            actualDrink
                .Id
                .Should()
                .Be(drink.Id);
        }

        [Fact]
        public async Task CountBySearchAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            // 100 drinks with 3 categories
            await this.SeedData(db, 100, 3);

            var drinkService = new DrinkService(db);

            const string Search = "0";

            var actualCount = await drinkService.CountBySearchAsync(Search);

            var expectedCount = await db
                .Drinks
                .Where(d => d.Name.ToLower().Contains(Search.ToLower()))
                .CountAsync();

            actualCount.Should().Be(expectedCount);
        }

        [Fact]
        public async Task AddAsyncShouldSaveDrinkToDb()
        {
            var db = DbInfrastructure.GetDatabase();

            var drinkService = new DrinkService(db);

            await AddDrinkToDb(drinkService);

            var actualDrink = await db.Drinks.FirstAsync();

            actualDrink.Name.Should().Be(Name);
            actualDrink.Description.Should().Be(Description);
            actualDrink.FullDescription.Should().Be(FullDescription);
            actualDrink.Price.Should().Be(Price);
            actualDrink.ImageUrl.Should().Be(ImageUrl);
            actualDrink.ImageThumbnailUrl.Should().Be(ImageThumbnailUrl);
            actualDrink.IsPreferred.Should().Be(true);
            actualDrink.IsInStock.Should().Be(false);
            actualDrink.CategoryId.Should().Be(CategoryId);
        }

        [Fact]
        public async Task EditAsyncShouldEditDrink()
        {
            var db = DbInfrastructure.GetDatabase();

            var drinkService = new DrinkService(db);

            await drinkService.AddAsync(
                "Some name",
                "Stupid description",
                "The full description",
                2.23M,
                "Image URL",
                "Image TURL",
                false,
                true,
                56);

            var actualDrink = await db.Drinks.FirstAsync();

            await drinkService.EditAsync(
                actualDrink.Id,
                Name,
                Description,
                FullDescription,
                Price,
                ImageUrl,
                ImageThumbnailUrl,
                true,
                false,
                CategoryId);

            actualDrink = await db.Drinks.FirstAsync();

            actualDrink.Name.Should().Be(Name);
            actualDrink.Description.Should().Be(Description);
            actualDrink.FullDescription.Should().Be(FullDescription);
            actualDrink.Price.Should().Be(Price);
            actualDrink.ImageUrl.Should().Be(ImageUrl);
            actualDrink.ImageThumbnailUrl.Should().Be(ImageThumbnailUrl);
            actualDrink.IsPreferred.Should().Be(true);
            actualDrink.IsInStock.Should().Be(false);
            actualDrink.CategoryId.Should().Be(CategoryId);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteDrink()
        {
            var db = DbInfrastructure.GetDatabase();

            var drinkService = new DrinkService(db);

            await AddDrinkToDb(drinkService);

            var drink = await db.Drinks.FirstAsync();

            await drinkService.DeleteAsync(drink.Id);

            drink = await db.Drinks.FirstOrDefaultAsync();

            drink.Should().BeNull();
        }

        private static async Task AddDrinkToDb(DrinkService drinkService)
        {
            await drinkService.AddAsync(
                Name,
                Description,
                FullDescription,
                Price,
                ImageUrl,
                ImageThumbnailUrl,
                true,
                false,
                CategoryId);
        }

        private async Task SeedDataByCategory(GDrinksDbContext db, int drinks, int categoryId)
        {
            for (var i = 0; i < drinks; i++)
            {
                await db.AddAsync(new Drink
                {
                    Name = $"Some Drink {i}",
                    CategoryId = categoryId
                });
            }

            await db.SaveChangesAsync();
        }

        private async Task SeedData(GDrinksDbContext db, int drinks, int categoriesCount, bool preffered = false)
        {
            var categories = new List<Category>();

            for (var i = 0; i < categoriesCount; i++)
            {
                var category = new Category
                {
                    Name = $"Some Category {i}"
                };

                await db.AddAsync(category);
                categories.Add(category);
            }

            await db.SaveChangesAsync();

            for (var i = 0; i < drinks; i++)
            {
                await db.AddAsync(new Drink
                {
                    Name = $"Some Drink {i}",
                    CategoryId = categories[this.random.Next(0, categories.Count)].Id,
                    IsPreferred = preffered
                });
            }

            await db.SaveChangesAsync();
        }
    }
}