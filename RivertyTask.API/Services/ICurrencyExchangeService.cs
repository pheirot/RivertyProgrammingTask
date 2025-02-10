namespace RivertyTask.API.Services
{
    public interface ICurrencyExchangeService
    {
        Task<decimal> GetExchangeRate(string from, string to, decimal amount, string? date);
    }
}