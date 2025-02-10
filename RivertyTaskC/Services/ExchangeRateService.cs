using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ExchangeRateService
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<decimal> GetExchangeRate(string fromCurrency, string toCurrency)
    {
        string apiKey = "your_api_key"; // Replace with your API key
        string url = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{fromCurrency}";

        try
        {
            var response = await client.GetStringAsync(url);
            var exchangeData = JsonConvert.DeserializeObject<ExchangeRateApiResponse>(response);

            if (exchangeData != null && exchangeData.ConversionRates.ContainsKey(toCurrency))
            {
                return exchangeData.ConversionRates[toCurrency];
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching exchange rate: " + ex.Message);
        }

        return 0;
    }

    public class ExchangeRateApiResponse
    {
        [JsonProperty("conversion_rates")]
        public Dictionary<string, decimal> ConversionRates { get; set; }
    }
}