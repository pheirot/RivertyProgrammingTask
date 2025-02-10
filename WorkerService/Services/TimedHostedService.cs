namespace WorkerService.Services
{
    public class TimedHostedService : BackgroundService
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IDatabaseService _databaseService;
        private readonly IExchangeRateService _exchangeRateService;

        private int _executionCount;
        private static readonly string[] _currencies = { "USD", "EUR", "JPY", "GBP", "NOK" }; //TODO: Move to configuration
        private readonly string _baseCurrency = "EUR"; //TODO: Move to configuration

        public TimedHostedService(ILogger<TimedHostedService> logger,
            IDatabaseService databaseService,
            IExchangeRateService exchangeRateService)
        {
            _logger = logger;
            _databaseService = databaseService;
            _exchangeRateService = exchangeRateService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            // When the timer should have no due-time, then do the work once now.
            await DoWork();

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(5));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await DoWork();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Timed Hosted Service is stopping.");
            }
        }

        private async Task DoWork()
        {
            int count = Interlocked.Increment(ref _executionCount);

            _logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);

            var currenciesList = string.Join(", ", _currencies);

            foreach (var currency in _currencies)
            {
                var exchangeRates = await _exchangeRateService.GetExchangeRateAsync(_baseCurrency, currenciesList);
                //TODO: Add Check response from exchangeRateService

                await _databaseService.SaveExchangeRateAsync(exchangeRates);
            }
        }
    }
}
