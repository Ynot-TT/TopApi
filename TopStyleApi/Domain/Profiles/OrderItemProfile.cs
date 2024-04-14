using AutoMapper;
using TopStyle.Domain.Entities;
using TopStyleApi.Domain.DTO;
using TopStyleApi.Domain.Entities;

namespace TopStyleApi.Domain.Profiles
{
    public class OrderItemProfile:Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, AddOrderItemDTO>();
            CreateMap<AddOrderItemDTO, OrderItem>();
        }
    }
}
