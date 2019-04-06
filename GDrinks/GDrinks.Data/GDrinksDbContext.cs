namespace GDrinks.Data
{
    using GDrinks.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class GDrinksDbContext : IdentityDbContext<User>
    {
        public GDrinksDbContext(DbContextOptions<GDrinksDbContext> options)
            : base(options)
        {
        }

        public DbSet<Drink> Drinks { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            builder
                .Entity<Category>()
                .HasMany(c => c.Drinks)
                .WithOne(d => d.Category)
                .HasForeignKey(d => d.CategoryId);

            builder
                .Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            builder
                .Entity<OrderItem>()
                .HasOne(oi => oi.Drink)
                .WithMany(d => d.OrderItems)
                .HasForeignKey(oi => oi.DrinkId);

            builder
                .Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            base.OnModelCreating(builder);
        }
    }
}