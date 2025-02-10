namespace RivertyTask.UI.Data
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public decimal Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
