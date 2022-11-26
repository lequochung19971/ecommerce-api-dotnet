using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Dtos;
using Ecommerce.Entities;
using Ecommerce.Extensions;

namespace Ecommerce.MappingProfiles;
public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Product, ProductDto>().ForMember(p => p.Sku, opt => opt.Ignore());
        CreateMap<Product, CreateProductDto>().ReverseMap().ForMember(p => p.Sku, option => option.MapFrom(p => p.Name.toKebabCase()));
    }
}