using Billing.Core.Interfaces;
using Billing.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Billing.Core.Services
{
    public class PaymentGatewayProvider : IGatewayProvider
    {
        private readonly IEnumerable<IPaymentGateway> _paymentGateways;

        public PaymentGatewayProvider(IEnumerable<IPaymentGateway> paymentGateways)
        {
            _paymentGateways = paymentGateways;
        }

        public IPaymentGateway GetGateway(PaymentGateways paymentGateway)
        {
            var gateway = _paymentGateways.FirstOrDefault(x => x.GatewayType == paymentGateway);

            if (gateway == null) {
                throw new ArgumentException($"Gateway {paymentGateway} is not supported");
            }

            return gateway;
        }
    }
}
