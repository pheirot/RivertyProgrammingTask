using WorkerService.Data;
using static ExchangeRateService;

public interface IExchangeRateService
{
    Task<ExchangeRateApiResponse> GetExchangeRateAsync(string fromCurrency, string toCurrency);
}