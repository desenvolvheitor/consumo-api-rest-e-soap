using System.Text.Json;
using System.Text.Json.Serialization;

namespace CotacaoDolarAPICalculadora
{
    public class Dolar
    {
        [JsonPropertyName("code")]
        public string AcronOrigin { get; set; }

        [JsonPropertyName("codein")]
        public string AcronDest { get; set; }

        [JsonPropertyName("name")]
        public string QuotName { get; set; }

        [JsonPropertyName("high")]
        public decimal HighPrice { get; set; }

        [JsonPropertyName("low")]
        public decimal LowPrice { get; set; }

        [JsonPropertyName("bid")]
        public decimal BuyPrice { get; set; }

        [JsonPropertyName("ask")]
        public decimal SellPrice { get; set; }

        [JsonPropertyName("create_date")]
        public string UpdateDate { get; set; }
    }
}
