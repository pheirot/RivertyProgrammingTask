using Microsoft.EntityFrameworkCore;
using RivertyTaskC.Models;
using System.Collections.Generic;

public class ExchangeRateDbContext : DbContext
{
    public DbSet<ExchangeRate> ExchangeRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Adjust your connection string here
        optionsBuilder.UseSqlServer("Server=your_server;Database=ExchangeRatesDb;Trusted_Connection=True;");
    }
}