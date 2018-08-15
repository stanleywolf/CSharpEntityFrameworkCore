using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using P01.ProductShop.Dtos;
using P01.ProductShop.Services;
using P01.ProductShop.Services.Entity;

namespace P01.ProductShop
{
    public class ProductShopProfile:Profile
    {
        public ProductShopProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<ExportDtos.CategoryDto, Category>().ReverseMap();
        }
    }
}
