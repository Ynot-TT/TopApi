using AutoMapper;
using TopStyle.Domain.Entities;
using TopStyleApi.Domain.DTO;

namespace TopStyleApi.Domain.Profiles
{
    public class OrderProfile:Profile

    {
        public OrderProfile()
        {
            CreateMap<Order, AddOrderDTO>();
            CreateMap<AddOrderDTO, Order>();




        }

    }
}
