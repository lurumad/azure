using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ManagedIdentity.Models
{
    public class CatalogDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public CatalogDbContext(IConfiguration configuration, DbContextOptions<CatalogDbContext> options) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = configuration.GetConnectionString("SqlServerConnection");
            sqlConnection.AccessToken =
                new AzureServiceTokenProvider().GetAccessTokenAsync("https://database.windows.net/").Result;
            optionsBuilder.UseSqlServer(sqlConnection);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
