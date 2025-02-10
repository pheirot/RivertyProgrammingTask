using RivertyTaskC.Models;

public class DatabaseService
{
    public async Task SaveExchangeRateAsync(string fromCurrency, string toCurrency)
    {
        var exchangeRateService = new ExchangeRateService();
        decimal rate = await exchangeRateService.GetExchangeRate(fromCurrency, toCurrency);

        if (rate > 0)
        {
            var exchangeRate = new ExchangeRate
            {
                CurrencyFrom = fromCurrency,
                CurrencyTo = toCurrency,
                Rate = rate,
                Timestamp = DateTime.UtcNow
            };

            using (var context = new ExchangeRateDbContext())
            {
                context.ExchangeRates.Add(exchangeRate);
                await context.SaveChangesAsync();
            }
        }
        else
        {
            Console.WriteLine("Failed to get the exchange rate.");
        }
    }
}