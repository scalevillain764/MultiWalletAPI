using System.Text.Json.Serialization;

namespace _yoo_kassa_dto
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
    }
}