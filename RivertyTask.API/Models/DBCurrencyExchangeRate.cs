using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RivertyTask.API.Models
{
    public class DBCurrencyExchangeRate
    {
        [Key]
        public int CurrencyExchangeRateId { get; set; }
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public DateTime RateDate { get; set; }

        [Column(TypeName = "decimal(20, 10)")]
        public decimal ExchangeRate { get; set; }

    }
}
