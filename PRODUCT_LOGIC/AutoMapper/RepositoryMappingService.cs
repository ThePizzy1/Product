using AutoMapper;
using PRODUCT_DATA.DataModel;
using PRODUCT_DOMAIN;
using PRODUCT_DOMAIN.DataModel;
using PRODUCT_LOGIC.Automaper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRODUCT_LOGIC.AutoMapper
{
    public class RepositoryMappingService : IRepositoryMappingService
    {
        public Mapper mapper;


        public RepositoryMappingService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Product
                cfg.CreateMap<Product, ProductDomain>();
                cfg.CreateMap<ProductDomain, Product>();

                // Cart
                cfg.CreateMap<Cart, CartDomain>();
                cfg.CreateMap<CartDomain, Cart>();

                // CartItem
                cfg.CreateMap<CartItem, CartItemDomain>()
                   .ForMember(dest => dest.Cart, opt => opt.Ignore()); 
                cfg.CreateMap<CartItemDomain, CartItem>()
                   .ForMember(dest => dest.Cart, opt => opt.Ignore());
            });

            mapper = new Mapper(config);
        }

        public TDestination Map<TDestination>(object source)
        {
            return mapper.Map<TDestination>(source);
        }
    }
}
