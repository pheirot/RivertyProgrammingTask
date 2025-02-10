using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using RivertyTask.Models;
using System.Net.Http.Json;

class Program
{
    private static IConfiguration _config;

    private static readonly string _urlLatest = "http://data.fixer.io/api/latest"; //TODO: move to appsettings.json ?
    private static readonly string _urlDate = "http://data.fixer.io/api/"; //TODO: move to appsettings.json ?

    static async Task Main(string[] args)
    {
        //Used to get secrets - better to use Secrets Manager (secrets.json)
        IConfigurationRoot config = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();

        _config = config;

        //Get user's input
        Console.WriteLine("Input first currency code (convert from): ");
        var firstCode = Console.ReadLine()?.ToUpper(); //TODO: validate input

        Console.WriteLine("Input second currency code (convert to): ");
        var secondCode = Console.ReadLine()?.ToUpper(); //TODO: validate input

        Console.WriteLine("Input amount in the first currency: ");
        var amountInput = Console.ReadLine();
        //Amount validations
        if (string.IsNullOrEmpty(amountInput) || !double.TryParse(amountInput, out var amount))
        {
            Console.WriteLine("Invalid amount input.");
            return;
        }

        Console.WriteLine("Input date (format YYYY-MM-DD) or leave it empty for the latest: ");
        string? date = Console.ReadLine(); //TODO: validate input

        //Get exchange rate
        var exchangeRate = await GetLatestExchangeRate(firstCode, secondCode, date);

        if (exchangeRate != null && exchangeRate.Rates != null)
        {
            var rate = exchangeRate.Rates.ContainsKey(secondCode) ? exchangeRate.Rates[secondCode] : 0;
            var convertedAmount = amount * rate;
            Console.WriteLine($"Converted amount: {convertedAmount}");
        }
        else
        {
            Console.WriteLine("Failed to retrieve exchange rate.");
        }
    }

    static async Task<CurrencyInfo?> GetLatestExchangeRate(string firstCode, string secondCode, string? date)
    {
        using var client = new HttpClient();

        //TODO: use Secrets Manager (secrets.json) to keep accessKey
        var accessKey = _config.GetSection("Access_key").Value;

        string urlParameters = $"{date}?access_key={accessKey}&base=EUR&symbols={firstCode},{secondCode}";
        var baseUrl = string.IsNullOrEmpty(date) ? _urlLatest : _urlDate;

        var response = await client.GetAsync(baseUrl + urlParameters);
        var t = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        var currencyInfo = await response.Content.ReadFromJsonAsync<CurrencyInfo>();

        return currencyInfo;
    }
}
