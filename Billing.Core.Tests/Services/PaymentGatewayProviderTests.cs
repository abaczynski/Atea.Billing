using Billing.Core.Interfaces;
using Billing.Core.Models;
using Billing.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Billing.Core.Tests.Services
{
    public class PaymentGatewayProviderTests
    {
        private Mock<IPaymentGateway> _paypalPaymentGateway;
        private Mock<IPaymentGateway> _payUPaymentGateway;

        private readonly IGatewayProvider _gatewayProvider;

        public PaymentGatewayProviderTests()
        {
            _paypalPaymentGateway = new Mock<IPaymentGateway>();
            _payUPaymentGateway = new Mock<IPaymentGateway>();

            _gatewayProvider = new PaymentGatewayProvider(new List<IPaymentGateway>() { _paypalPaymentGateway.Object, _payUPaymentGateway.Object });
        }


        [Fact]
        public void GetGateway_WhenPaymentGatewayIsNotFound_ShouldThrowException()
        {
            // Arrange
            _paypalPaymentGateway.SetupGet(x => x.GatewayType).Returns(Models.PaymentGateways.PayPal);

            // Act
            Func<IPaymentGateway> result = () => _gatewayProvider.GetGateway(PaymentGateways.PayU);

            // Assert
            Assert.Throws<ArgumentException>(result);
        }

        [Fact]
        public void GetGateway_WhenAskingForPayPal_ShouldReturnPaypalGateway()
        {
            // Arrange
            _paypalPaymentGateway.SetupGet(x => x.GatewayType).Returns(Models.PaymentGateways.PayPal);
            _payUPaymentGateway.SetupGet(x => x.GatewayType).Returns(Models.PaymentGateways.PayU);

            // Act
            var gateway = _gatewayProvider.GetGateway(PaymentGateways.PayPal);

            // Assert
            Assert.Equal(_paypalPaymentGateway.Object, gateway);
        }

        [Fact]
        public void GetGateway_WhenAskingForPayU_ShouldReturnPayU()
        {
            // Arrange
            _paypalPaymentGateway.SetupGet(x => x.GatewayType).Returns(Models.PaymentGateways.PayPal);
            _payUPaymentGateway.SetupGet(x => x.GatewayType).Returns(Models.PaymentGateways.PayU);

            // Act
            var gateway = _gatewayProvider.GetGateway(PaymentGateways.PayU);

            // Assert
            Assert.Equal(_payUPaymentGateway.Object, gateway);
        }
    }
}
