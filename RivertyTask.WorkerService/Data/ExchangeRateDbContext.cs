using Microsoft.EntityFrameworkCore;
using WorkerService.Data;

public class ExchangeRateDbContext : DbContext
{
    public DbSet<ExchangeRate> ExchangeRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Adjust connection string 
        optionsBuilder.UseSqlServer("Server=my_local_server;Database=ExchangeRatesDb;Trusted_Connection=True;");
    }
}