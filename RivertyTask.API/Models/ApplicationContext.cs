using Microsoft.EntityFrameworkCore;

namespace RivertyTask.API.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DBCurrencyExchangeRate> CurrencyExchangeRate { get; set; }
        public DbSet<DBCurrency> Currencies { get; set; }

        private readonly Settings _settings;

        public ApplicationContext(Settings settings)
        {
            _settings = settings;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(_settings.DB_url);

        }
    }
}
