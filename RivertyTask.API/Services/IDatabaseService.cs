namespace RivertyTask.API.Services
{
    public interface IDatabaseService
    {
        Task<decimal> GetExchangeRates(string currency, string dateFrom, string dateTo);
    }
}