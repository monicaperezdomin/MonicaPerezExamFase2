using Newtonsoft.Json;

namespace PVueling.Domain.Entities
{
    public class Transaction
    {
        [JsonIgnore]
        public int id { get; set; }
        public string sku { get; set; }
        public string currency { get; set; }
        public decimal amount { get; set; }

    }
}
