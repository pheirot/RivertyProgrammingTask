using Microsoft.EntityFrameworkCore;

namespace RivertyTask.API.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DBCurrencyExchangeRate> CurrencyExchangeRate { get; set; }
        public DbSet<DBCurrency> Currencies { get; set; }

        private readonly IConfiguration _configuration;
        public ApplicationContext(IConfiguration configuration)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(_configuration.GetValue<string>("DB_url"));
        }
    }
}
