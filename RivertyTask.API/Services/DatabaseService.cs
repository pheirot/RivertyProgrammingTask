
namespace RivertyTask.API.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _dbUrl;
        public DatabaseService(IConfiguration configuration) 
        {
            _dbUrl = configuration.GetValue<string>("DB_url"); //Get the DB url from secrets
        }

        public Task<decimal> GetExchangeRates(string currency, string dateFrom, string dateTo)
        {
            throw new NotImplementedException();
        }
    }
}
