using System.Text.Json.Serialization;

namespace RivertyTask.Models
{
    class CurrencyInfo
    {
        [JsonPropertyName("timestamp")]
        public int? TimeStamp { get; set; }

        [JsonPropertyName("base")]
        public string? Base { get; set; }

        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }


        [JsonPropertyName("rates")]
        public Dictionary<string, double>? Rates { get; set; }
    }

}
