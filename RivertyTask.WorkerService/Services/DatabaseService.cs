using Microsoft.IdentityModel.Tokens;
using WorkerService.Data;

public class DatabaseService : IDatabaseService
{
    public async Task SaveExchangeRateAsync(IEnumerable<ExchangeRate> exchangeRates)
    {
        if (!exchangeRates.IsNullOrEmpty())
        {
            using (var context = new ExchangeRateDbContext())
            {
                context.ExchangeRates.AddRange(exchangeRates);
                await context.SaveChangesAsync();
            }
        }
        else
        {
            Console.WriteLine("Failed to save the exchange rate.");
        }
    }
}