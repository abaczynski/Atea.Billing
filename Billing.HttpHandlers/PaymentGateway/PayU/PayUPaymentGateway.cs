using Billing.Core.Interfaces;
using Billing.Core.Models;
using System.Threading.Tasks;

namespace Billing.HttpHandlers.PaymentGateway.PayU
{
    public class PayUPaymentGateway : IPaymentGateway
    {
        public PaymentGateways GatewayType => PaymentGateways.PayU;

        public async Task<string> ProcessPaymentAsync(object order)
        {
            return await Task.FromResult("1233-payU");
        }
    }
}
