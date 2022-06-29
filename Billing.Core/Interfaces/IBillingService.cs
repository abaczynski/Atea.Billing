using Billing.Core.Models;
using System.Threading.Tasks;

namespace Billing.Core.Interfaces
{
    public interface IBillingService
    {
        Task<string> ProcessOrderAsync(Order order);
    }
}
