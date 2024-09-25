using AutoMapper;
using Prices.Data.Models;

namespace Prices.Models.Profiles
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<StockEntity, StockDto>();
            CreateMap<StockDto, StockEntity>();
        }
    }
}
