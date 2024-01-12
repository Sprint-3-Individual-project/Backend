using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebshopBackend.DTOs
{
    public class UpdatePriceDTO
    {
        [Newtonsoft.Json.JsonConstructor]
        public UpdatePriceDTO(double newPrice)
        {
            NewPrice = newPrice;
        }

        [JsonProperty("newPrice")]
        public double NewPrice { get; set; }
    }
}
