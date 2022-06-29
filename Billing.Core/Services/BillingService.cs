using Billing.Core.Interfaces;
using Billing.Core.Models;
using System.Threading.Tasks;

namespace Billing.Core.Services
{
    public class BillingService : IBillingService
    {
        private readonly IGatewayProvider _paymentGatewayProvider;
        private readonly IReceiptBuilder _receiptBuilder;

        public BillingService(IGatewayProvider paymentGatewayProvider, IReceiptBuilder receiptBuilder)
        {
            _paymentGatewayProvider = paymentGatewayProvider;
            _receiptBuilder = receiptBuilder;
        }

        public async Task<string> ProcessOrderAsync(Order order)
        {
            var paymentGateway = _paymentGatewayProvider.GetGateway(order.PaymentProvider);

            var paymentReferenceNumber = await paymentGateway.ProcessPaymentAsync(order);

            return _receiptBuilder.BuildReceipt(order, paymentReferenceNumber);
        }
    }
}
