namespace GDrinks.Services
{
    using Microsoft.AspNetCore.Builder;

    public interface IDbSeederService
    {
        void SeedData(IApplicationBuilder applicationBuilder);
    }
}