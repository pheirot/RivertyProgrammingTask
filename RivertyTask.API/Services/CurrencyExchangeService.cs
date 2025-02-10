using Newtonsoft.Json;
using System.Globalization;


namespace RivertyTask.API.Services
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly IConfiguration _config;

        public CurrencyExchangeService(IConfiguration configuration)
        {
            _config = configuration;            
        }

        public async Task<decimal> GetExchangeRate(string fromCurrency, string toCurrency, decimal amount, string date = "")
        {
            var accessKey = _config.GetValue<string>("Access_key");
            var Url_latest = _config.GetSection("AppSettings").GetValue<string>("Url_latest");
            var Url_date = _config.GetSection("AppSettings").GetValue<string>("Url_date");

            using var client = new HttpClient();

            var baseUrl = string.IsNullOrEmpty(date) ? Url_latest : Url_date;
            string urlParameters = $"{date}?access_key={accessKey}&base={fromCurrency}&symbols={toCurrency}";

            var response = await client.GetAsync(baseUrl + urlParameters);
            response.EnsureSuccessStatusCode();

            var responceString = await response.Content.ReadAsStringAsync();

            var exchangeData = JsonConvert.DeserializeObject<ExchangeRateApiResponse>(responceString);
            //check success

            if (exchangeData != null && exchangeData.Rates.TryGetValue(toCurrency, out decimal value))
            {
                return value * amount;
            }

            // Return 0 or throw an exception if the exchange rate is not found
            return 0;
        }

        //TODO: Implement the following method
        //public async Task<IEnumerable<ExchangeRate>> GetExchangeRatesFromDBAsync(string currency, string dateFrom, string dateTo)

    }
}
