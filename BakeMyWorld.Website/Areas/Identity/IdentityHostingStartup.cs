using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: HostingStartup(typeof(BakeMyWorld.Website.Areas.Identity.IdentityHostingStartup))]
namespace BakeMyWorld.Website.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
