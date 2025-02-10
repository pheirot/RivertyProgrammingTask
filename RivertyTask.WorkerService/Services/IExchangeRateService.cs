using WorkerService.Data;

public interface IExchangeRateService
{
    Task<ExchangeRate> GetExchangeRateAsync(string fromCurrency, string toCurrency);
}