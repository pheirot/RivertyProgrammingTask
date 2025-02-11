using Newtonsoft.Json;
using WorkerService.Data;

public partial class ExchangeRateService : IExchangeRateService
{
    //TODO: Add and use ILogger
    private static readonly HttpClient client = new HttpClient();
    private static readonly string _baseUrl = "http://data.fixer.io/api/latest"; //TODO: move to appsettings.json or use secrets.json

    public async Task<ExchangeRateApiResponse> GetExchangeRateAsync(string fromCurrency, string toCurrencies)
    {
        string apiKey = "my_api_key"; // TODO: Replace with API key, store apiKey in secrets.json (use Secrets Manager)
        string url = $"{_baseUrl}?access_key={apiKey}&base={fromCurrency}&symbols={toCurrencies}";

        try
        {
            using var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responceString = await response.Content.ReadAsStringAsync();
            var exchangeData = JsonConvert.DeserializeObject<ExchangeRateApiResponse>(responceString);
            //TODO: check success
            return exchangeData;
        }
        catch (Exception ex)
        {
            //TODO: Add logging
            Console.WriteLine("Error fetching exchange rate: " + ex.Message);
        }

        return null; // Ensure a value is returned in all code paths
    }
}