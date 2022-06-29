using AutoMapper;
using Billing.Api.Models;
using Billing.Core.Models;

namespace Billing.Api.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderRequest, Order>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.OrderNumber))
                .ForMember(x => x.TotalAmount, opt => opt.MapFrom(y => y.PayableAmount));
        }
    }
}
