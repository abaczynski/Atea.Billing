using Billing.Core.Models;

namespace Billing.Core.Interfaces
{
    public interface IGatewayProvider
    {
        IPaymentGateway GetGateway(PaymentGateways paymentGateway);
    }
}
