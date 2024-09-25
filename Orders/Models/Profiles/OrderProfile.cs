using AutoMapper;
using Orders.Data.Models;

namespace Orders.Models.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderEntity, OrderDto>()
               .ForMember(x => x.Side, opt => opt.MapFrom(src => src.Side.ToString()));
        }
    }
}
