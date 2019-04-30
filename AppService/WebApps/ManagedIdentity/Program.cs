using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ManagedIdentity.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ManagedIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .MigrateDbContext<CatalogDbContext>(context =>
                {
                    context.Products.Add(new Product { Name = "HP ZBook" });
                    context.Products.Add(new Product { Name = "HP ZBook 2" });
                    context.Products.Add(new Product { Name = "HP ZBook 3" });
                    context.SaveChanges();
                }).Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
