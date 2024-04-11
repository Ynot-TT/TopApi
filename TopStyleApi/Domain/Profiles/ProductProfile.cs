using AutoMapper;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;
using TopStyleApi.Domain.DTO;

namespace TopStyleApi.Domain.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
