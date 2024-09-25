using AutoMapper;
using Prices.Data.Models;

namespace Prices.Models.Profiles
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<StockEntity, StockInputModel>();
            CreateMap<StockInputModel, StockEntity>();
        }
    }
}
