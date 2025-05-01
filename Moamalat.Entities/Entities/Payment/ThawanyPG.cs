using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Moamalat.Entities
{
    public class PaymentProduct
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("unit_amount")]
        public int UnitAmount { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }

    public class PaymentSessionData
    {
        [JsonPropertyName("mode")]
        public string? Mode { get; set; }

        [JsonPropertyName("session_id")]
        public string? SessionId { get; set; }

        [JsonPropertyName("client_reference_id")]
        public string? ClientReferenceId { get; set; }

        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }  // Nullable

        [JsonPropertyName("products")]
        public List<PaymentProduct>? Products { get; set; }

        [JsonPropertyName("total_amount")]
        public int TotalAmount { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("success_url")]
        public string? SuccessUrl { get; set; }

        [JsonPropertyName("cancel_url")]
        public string? CancelUrl { get; set; }

        [JsonPropertyName("return_url")]
        public string? ReturnUrl { get; set; } // Nullable

        [JsonPropertyName("payment_status")]
        public string? PaymentStatus { get; set; }

        [JsonPropertyName("invoice")]
        public string? Invoice { get; set; }

        [JsonPropertyName("save_card_on_success")]
        public bool SaveCardOnSuccess { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, object>? Metadata { get; set; }

        [JsonPropertyName("is_cvv_required")]
        public bool IsCvvRequired { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("expire_at")]
        public DateTime ExpireAt { get; set; }
    }

    public class PGResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("data")]
        public PaymentSessionData? Data { get; set; }

        public PaymentRequest? OriginalPaymentRequest { get; set; }
    }
}
