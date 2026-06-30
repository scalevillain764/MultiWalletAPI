using System.Text.Json.Serialization;

namespace _yoo_cass_dto
{
    public class YooKassaDTO
    {
        [JsonPropertyName("object")]
        public YooKassaPaymentObject Object { get; set; }

    }

    public class YooKassaPaymentObject
    {
        [JsonPropertyName("id")]
        public string PaymentId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("paid")]
        public bool Paid { get; set; }

        [JsonPropertyName("amount")]
        public Amount Amount { get; set; }
    }

    public class Amount
    {
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}