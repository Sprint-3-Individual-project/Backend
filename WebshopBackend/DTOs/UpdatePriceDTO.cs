using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebshopBackend.DTOs
{
    public class UpdatePriceDTO
    {
        [Newtonsoft.Json.JsonConstructor]
        public UpdatePriceDTO([JsonProperty("newPrice")] double newPrice)
        {
            NewPrice = newPrice;
        }

        public double NewPrice { get; set; }
    }
}
