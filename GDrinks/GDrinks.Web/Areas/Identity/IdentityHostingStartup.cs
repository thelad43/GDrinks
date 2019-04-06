using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(GDrinks.Web.Areas.Identity.IdentityHostingStartup))]

namespace GDrinks.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}