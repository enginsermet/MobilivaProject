using AutoMapper;
using MobilivaProject.Entities;
using MobilivaProject.Models;

namespace MobilivaProject.Configurations
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<CreateOrderRequest, OrderDetail>();
        }
    }
}
