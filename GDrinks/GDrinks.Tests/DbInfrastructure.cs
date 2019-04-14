namespace GDrinks.Tests
{
    using GDrinks.Data;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class DbInfrastructure
    {
        public static GDrinksDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<GDrinksDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new GDrinksDbContext(dbOptions);
        }
    }
}