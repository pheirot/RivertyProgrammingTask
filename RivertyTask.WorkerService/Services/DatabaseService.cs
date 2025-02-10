using WorkerService.Data;

public class DatabaseService : IDatabaseService
{
    public async Task SaveExchangeRateAsync(ExchangeRate exchangeRate)
    {
        if (exchangeRate != null)
        {
            using (var context = new ExchangeRateDbContext())
            {
                context.ExchangeRates.Add(exchangeRate);
                await context.SaveChangesAsync();
            }
        }
        else
        {
            Console.WriteLine("Failed to save the exchange rate.");
        }
    }
}