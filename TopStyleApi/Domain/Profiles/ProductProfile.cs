using AutoMapper;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;
using TopStyleApi.Domain.DTO;

namespace TopStyleApi.Domain.Profiles
{
    public class Product:Profile
    {
        public Product()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
