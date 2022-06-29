using AutoMapper;
using Billing.Api.Models;
using Billing.Core.Interfaces;
using Billing.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Billing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IBillingService _billingService;
        private readonly IMapper _mapper;

        public OrderController(IBillingService billingService, IMapper mapper)
        {
            _billingService = billingService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessOrderAsync(OrderRequest orderRequest)
        {
            var order = _mapper.Map<Order>(orderRequest);

            var receiptDescription =  await _billingService.ProcessOrderAsync(order);

            return Ok(new ReceiptResponse()
            {
                ReceiptDescription = receiptDescription,
                TransactionDate = new System.DateTime()
            });
        }
    }
}
