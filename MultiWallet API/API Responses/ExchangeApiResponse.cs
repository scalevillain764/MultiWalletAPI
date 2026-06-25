using System.Text.Json.Serialization;

namespace _exchange_response
{
    public class ExchangeApiResponse
    {
        [JsonPropertyName("conversion_rates")]
        public Dictionary<string, decimal> ConversionRates { get; set; } = new();
    }
}