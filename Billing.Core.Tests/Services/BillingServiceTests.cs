using Billing.Core.Interfaces;
using Billing.Core.Models;
using Billing.Core.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Billing.Core.Tests.Services
{
    public class BillingServiceTests
    {
        private Mock<IGatewayProvider> _gatewayProviderMock { get; set; }
        private Mock<IPaymentGateway> _paymentGateway { get; set; }
        private Mock<IReceiptBuilder> _receiptBuilderMock { get; set; }

        private readonly IBillingService _billingService;

        public BillingServiceTests()
        {
            _gatewayProviderMock = new Mock<IGatewayProvider>();
            _receiptBuilderMock = new Mock<IReceiptBuilder>();
            _paymentGateway = new Mock<IPaymentGateway>();

            _billingService = new BillingService(_gatewayProviderMock.Object, _receiptBuilderMock.Object);
        }

        [Fact]
        public async Task ProcessOrderAsync_WhenOrderPaymentTypeIsPayPal_ShouldGetPayPalPaymentProvider() 
        {
            // Arrange
            var order = new Order() { 
                PaymentProvider = PaymentGateways.PayPal
            };

            _gatewayProviderMock.Setup(x => x.GetGateway(It.IsAny<PaymentGateways>())).Returns(_paymentGateway.Object);

            // Act
            var result = await _billingService.ProcessOrderAsync(order);

            // Assert
            _gatewayProviderMock.Verify(x => x.GetGateway(PaymentGateways.PayPal), Times.Once);
        }

        [Fact]
        public async Task ProcessOrderAsync_WhenOrderPaymentTypeIsPayPal_ShouldUsePayPalPaymentGatewayWithOrder()
        {
            // Arrange
            var order = new Order()
            {
                PaymentProvider = PaymentGateways.PayPal
            };

            _gatewayProviderMock.Setup(x => x.GetGateway(It.IsAny<PaymentGateways>())).Returns(_paymentGateway.Object);

            // Act
            var result = await _billingService.ProcessOrderAsync(order);

            // Assert
            _paymentGateway.Verify(x => x.ProcessPaymentAsync(order), Times.Once);
        }

        [Fact]
        public async Task ProcessOrderAsync_WhenOrderPaymentTypeIsPayPal_ShouldBuild()
        {
            // Arrange
            var paymentReference = "ref123";

            var order = new Order()
            {
                PaymentProvider = PaymentGateways.PayPal
            };

            _gatewayProviderMock.Setup(x => x.GetGateway(It.IsAny<PaymentGateways>())).Returns(_paymentGateway.Object);
            _paymentGateway.Setup(x => x.ProcessPaymentAsync(It.IsAny<Order>())).ReturnsAsync(paymentReference);

            // Act
            var result = await _billingService.ProcessOrderAsync(order);

            // Assert
            _receiptBuilderMock.Verify(x => x.BuildReceipt(order, paymentReference), Times.Once);
        }
    }
}
