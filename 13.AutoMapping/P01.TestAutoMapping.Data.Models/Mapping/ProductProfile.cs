using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace P01.TestAutoMapping.Data.Models.Mapping
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(output => output.InStorageCount,
                    opt => opt
                        .MapFrom(src => src.ProductStocks
                            .Sum(x => x.Quantity)))

                .ForMember(output => output.ExpireMonth,
                    opt => opt
                        .MapFrom(src => src.ExpireDate.Month))

                .ForMember(output => output.ExpireYear,
                    opt => opt
                        .MapFrom(src => src.ExpireDate.Year))
                .ReverseMap();   
        }
    }
}
