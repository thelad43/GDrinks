namespace GDrinks.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class GDrinksDbContext : IdentityDbContext
    {
        public GDrinksDbContext(DbContextOptions<GDrinksDbContext> options)
            : base(options)
        {
        }
    }
}