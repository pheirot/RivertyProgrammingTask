using Microsoft.AspNetCore.Mvc;
using RivertyTask.API.Services;


namespace RivertyTask.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        //TODO: Add logger
        private readonly ICurrencyExchangeService _currencyExchangeService;
        private readonly IDatabaseService _databaseService;


        public CurrencyExchangeController(ICurrencyExchangeService exchangeService, IDatabaseService databaseService)
        {
            _currencyExchangeService = exchangeService;
            _databaseService = databaseService;
        }

        [HttpGet("calculate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetExchangeRate(string from, string to, decimal amount, string? date)
        {
            try
            {
                var result = await _currencyExchangeService.GetExchangeRate(from, to, amount, date ?? "");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("fetch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetExchangeRatesFromDB(string currency, string dateFrom, string dateTo)
        {
            try
            {
                var result = await _databaseService.GetExchangeRates(currency, dateFrom, dateTo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}