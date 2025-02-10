using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RivertyTask.API.Models;
using System.Collections.Generic;
using System.Globalization;


namespace RivertyTask.API.Services
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly Settings _config;
        //private readonly ApplicationContext _context;

        public CurrencyExchangeService(IOptions<Settings> configurationSection)
        {
            _config = configurationSection.Value;
        }

        public async Task<decimal> GetExchangeRate(string fromCurrency, string toCurrency, decimal amount, string date = "")
        {
            using var client = new HttpClient();
            var baseUrl = string.IsNullOrEmpty(date) ? _config?.Url_latest : _config?.Url_date;
            string urlParameters = $"{date}?access_key={_config.Access_key}&base={fromCurrency}&symbols={toCurrency}";

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

        //public async Task<IEnumerable<ExchangeRate>> GetExchangeRatesFromDBAsync(string currency, string dateFrom, string dateTo)
        //{

        //    //DataBase
        //    using ApplicationContext db = new(_config);

        //    _context = db;

        //    var result = new List<ExchangeRates>();

        //    //var currencyId = currencies.Where(x => x.ISO_Code == currency).First().CurrencyId;
        //    var currencyId = db.Currencies.FromSql($"SELECT * FROM [Currencies] WHERE ISO_Code = {currency}").First().CurrencyId;

        //    var exchanges = db.CurrencyExchangeRate.FromSql<DBCurrencyExchangeRate>($"SELECT * FROM dbo.CurrencyExchangeRate")
        //        .Where(x => x.RateDate >= ConvertToDateTime(dateFrom) && x.RateDate <= ConvertToDateTime(dateTo)).ToList();

        //    var filtered = exchanges;

        //    //date, Dict(ISO, rate)
        //    var dict = new Dictionary<DateTime, Dictionary<string, double>>();

        //    foreach (var x in filtered)
        //    {
        //        dict[x.RateDate] = new Dictionary<string, double>();
        //    }

        //    foreach (var date in dict.Keys)
        //    {
        //        var dateDataSet = filtered.Where(x => x.RateDate == date).ToList();

        //        var b = dateDataSet.Where(x => x.FromCurrencyId == currencyId).First().ExchangeRate;

        //        dict[date]["EUR"] = (double)b;

        //        foreach (var data in dateDataSet)
        //        {
        //            var d = data.ExchangeRate;
        //            var rate = (double)b / (double)d;
        //            dict[date][GetCurrencyISO(data.FromCurrencyId)] = rate;
        //        }

        //    }

        //    foreach (var x in dict)
        //    {
        //        result.Add(new ExchangeRate()
        //        {
        //            Date = x.Key,
        //            Rates = x.Value,
        //        });
        //    }

        //    return result.ToArray();
        //}

        //TODO: Move to a helper class
        private DateTime ConvertToDateTime(string date)
        {
            return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        //TODO: Move to a helper class
        //private string GetCurrencyISO(int currencyId)
        //{
        //    return _context.Currencies.FromSql($"SELECT * FROM [Currencies] WHERE CurrencyId = {currencyId}").First().ISO_Code;
        //}
    }
}
