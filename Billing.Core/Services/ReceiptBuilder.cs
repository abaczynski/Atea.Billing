using Billing.Core.Interfaces;
using Billing.Core.Models;

namespace Billing.Core.Services
{
    public class ReceiptBuilder : IReceiptBuilder
    {
        public string BuildReceipt(Order order, string paymentReferenceNumber)
        {
            return $"Total Amount: {order.TotalAmount}, PaymentReference: {paymentReferenceNumber}";
        }
    }
}
