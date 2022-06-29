using AutoMapper;
using Billing.Api.Controllers;
using Billing.Api.Models;
using Billing.Core.Interfaces;
using Billing.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Billing.Api.Tests.Controllers
{
    public class OrderControllerTests
    {
        private Mock<IBillingService> _billingServiceMock;
        private Mock<IMapper> _mapperMock;

        private readonly OrderController _orderController;

        public OrderControllerTests()
        {
            _billingServiceMock = new Mock<IBillingService>();
            _mapperMock = new Mock<IMapper>();

            _orderController = new OrderController(_billingServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ProcessOrderAsync_WhenOrderIsValid_ShouldReturnReceiptResponse_WithReceipt()
        {
            // Arrange
            var receiptDescription = "test1234";
            var orderRequest = new Models.OrderRequest() { PaymentGateway = Core.Models.PaymentGateways.PayPal };

            _mapperMock.Setup(x => x.Map<Order>(It.IsAny<OrderRequest>())).Returns(new Order() { PaymentProvider = PaymentGateways.PayPal });
            _billingServiceMock.Setup(x => x.ProcessOrderAsync(It.IsAny<Order>())).ReturnsAsync(receiptDescription);

            // Act
            var response = await _orderController.ProcessOrderAsync(orderRequest);

            // Assert
            var result = ((ReceiptResponse)((OkObjectResult)response).Value).ReceiptDescription;
            Assert.Equal(receiptDescription, result);
        }
    }
}
