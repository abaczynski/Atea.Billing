using Billing.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Billing.Api.Models
{
    public class OrderRequest
    {
        [Required]
        public int OrderNumber { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public double PayableAmount { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentGateways PaymentGateway { get; set; }
        public string Description { get; set; }
    }
}
