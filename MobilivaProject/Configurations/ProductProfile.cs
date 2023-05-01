using AutoMapper;
using MobilivaProject.DTOs;
using MobilivaProject.Entities;

namespace MobilivaProject.Configurations
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
        }
    }
}
