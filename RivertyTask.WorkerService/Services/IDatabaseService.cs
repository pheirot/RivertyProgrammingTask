using WorkerService.Data;

public interface IDatabaseService
{
    Task SaveExchangeRateAsync(IEnumerable<ExchangeRate> exchangeRates);
}