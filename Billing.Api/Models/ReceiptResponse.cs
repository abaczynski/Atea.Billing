using System;

namespace Billing.Api.Models
{
    public class ReceiptResponse
    {
        public DateTime TransactionDate{ get; set; }
        public string ReceiptDescription { get; set; }
    }
}
