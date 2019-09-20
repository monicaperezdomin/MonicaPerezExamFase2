using Newtonsoft.Json;

namespace PVueling.Domain.Entities
{
    public class Rate
    {
       [JsonIgnore]
       public int id { get; set; }
       public string from { get; set; }
       public decimal rate { get; set; }
       public string To { get; set; }

        
    }
}
