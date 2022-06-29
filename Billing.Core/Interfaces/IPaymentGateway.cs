using Billing.Core.Models;
using System.Threading.Tasks;

namespace Billing.Core.Interfaces
{
    public interface IPaymentGateway
    {
        PaymentGateways GatewayType { get; }
        Task<string> ProcessPaymentAsync(object order);
    }
}
