namespace GDrinks.Tests.Services
{
    using FluentAssertions;
    using GDrinks.Data;
    using GDrinks.Models;
    using GDrinks.Services;
    using GDrinks.Services.Implementations;
    using GDrinks.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class OrderServiceTests
    {
        private const string Address = "Address";
        private const string ZipCode = "Zip code";
        private const string Country = "Country";

        public OrderServiceTests()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllAsyncShouldReturnOrdersByPage()
        {
            var db = DbInfrastructure.GetDatabase();

            var orderService = new OrderService(db);

            await this.SeedOrder(db, orderService);

            var orders = await orderService.AllAsync(1);

            orders
                .Should()
                .HaveCount(1);

            orders
                .First()
                .AddressLine
                .Should()
                .Be(Address);

            orders
                .First()
                .Country
                .Should()
                .Be(Country);

            orders
                .First()
                .ZipCode
                .Should()
                .Be(ZipCode);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateNewOrderAndAddItemsToDb()
        {
            var db = DbInfrastructure.GetDatabase();

            var orderService = new OrderService(db);

            await this.SeedOrder(db, orderService);

            var user = await db.Users.FirstAsync();
            var order = await db.Orders.FirstAsync();

            order
                .AddressLine
                .Should()
                .Be(Address);

            order
                .ZipCode
                .Should()
                .Be(ZipCode);

            order
                .Country
                .Should()
                .Be(Country);

            order
                .UserId
                .Should()
                .Be(user.Id);

            var items = order.OrderItems;

            foreach (var item in items)
            {
                item
                    .OrderId
                    .Should()
                    .Be(order.Id);

                item
                    .Amount
                    .Should()
                    .BeInRange(2, 4);

                item
                    .Price
                    .Should()
                    .BeInRange(10, 30);
            }
        }

        [Fact]
        public async Task CountAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            var orderService = new OrderService(db);

            await this.SeedOrder(db, orderService);

            var count = await orderService.CountAsync();

            count.Should().Be(1);
        }

        [Fact]
        public async Task ItemsByOrderIdAsyncShouldReturnItemsByOrderId()
        {
            var db = DbInfrastructure.GetDatabase();

            var orderService = new OrderService(db);

            await this.SeedOrder(db, orderService);

            var order = await db.Orders.FirstAsync();

            var items = await orderService.ItemsByOrderIdAsync(order.Id);

            foreach (var item in items)
            {
                item
                    .Price
                    .Should()
                    .BeInRange(10, 30);

                item
                    .Amount
                    .Should()
                    .BeInRange(2, 4);
            }
        }

        private async Task SeedOrder(GDrinksDbContext db, IOrderService orderService)
        {
            var user = new User
            {
                UserName = "Some User"
            };

            await db.AddAsync(user);
            await db.SaveChangesAsync();

            var rakia = new Drink
            {
                Name = "Rakia",
                Price = 10
            };

            var water = new Drink
            {
                Name = "Water",
                Price = 20
            };

            var beer = new Drink
            {
                Name = "Beer",
                Price = 30
            };

            await db.AddRangeAsync(rakia, water, beer);
            await db.SaveChangesAsync();

            var cart = new ShoppingCart(db)
            {
                Items = new List<CartItem>
                {
                    new CartItem
                    {
                        DrinkId = rakia.Id,
                        Amount = 2,
                    },
                    new CartItem
                    {
                        DrinkId = water.Id,
                        Amount = 3,
                    },
                    new CartItem
                    {
                        DrinkId = beer.Id,
                        Amount = 4,
                    }
                }
            };

            await orderService.CreateAsync(Address, ZipCode, Country, user.Id, cart);
        }
    }
}