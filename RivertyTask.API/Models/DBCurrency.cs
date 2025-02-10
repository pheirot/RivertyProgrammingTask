using System.ComponentModel.DataAnnotations;

namespace RivertyTask.API.Models
{
    public class DBCurrency
    {
        [Key]
        public int CurrencyId { get; set; }
        public string? ISO_Code { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
    }

}
