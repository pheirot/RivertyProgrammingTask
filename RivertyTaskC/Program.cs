class Program
{
    private static readonly string[] _currencies = { "USD", "EUR", "JPY", "GBP", "NOK"};
    static void Main(string[] args)
    {
        Timer timer = new Timer(RunDailyTask, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }

    static async void RunDailyTask(object state)
    {
        var saver = new DatabaseService();
        foreach (var currency in _currencies)
        {
            await saver.SaveExchangeRateAsync("NOK", currency);
        }   
    }
}