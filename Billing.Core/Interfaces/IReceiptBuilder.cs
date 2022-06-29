using Billing.Core.Models;

namespace Billing.Core.Interfaces
{
    public interface IReceiptBuilder
    {
        string BuildReceipt(Order order, string paymentReferenceNumber);
    }
}
