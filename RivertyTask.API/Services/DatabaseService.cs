
namespace RivertyTask.API.Services
{
    public class DatabaseService : IDatabaseService
    {
        public DatabaseService() 
        {

        }

        public Task<decimal> GetExchangeRates(string currency, string dateFrom, string dateTo)
        {
            throw new NotImplementedException();
        }
    }
}
