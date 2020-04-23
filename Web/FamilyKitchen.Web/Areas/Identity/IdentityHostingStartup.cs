using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(FamilyKitchen.Web.Areas.Identity.IdentityHostingStartup))]

namespace FamilyKitchen.Web.Areas.Identity
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
