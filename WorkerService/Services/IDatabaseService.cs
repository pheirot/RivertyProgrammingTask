using WorkerService.Data;

public interface IDatabaseService
{
    Task SaveExchangeRateAsync(ExchangeRate exchangeRate);
}