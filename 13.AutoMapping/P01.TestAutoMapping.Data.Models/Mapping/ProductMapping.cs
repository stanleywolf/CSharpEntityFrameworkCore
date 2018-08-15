using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace P01.TestAutoMapping.Data.Models.Mapping
{
    public static class ProductMapping
    {
        public static ProductDTO MapToProductDto(Product product)
        {
            return new ProductDTO()
            {
                Name = product.Name,
                InStorageCount = product.ProductStocks
                    .Sum(x => x.Quantity)
            };
        }

        public static ProductDTO ToProductDTO(this Product product)
        {
            return new ProductDTO()
            {
                Name = product.Name,
                InStorageCount = product.ProductStocks
                    .Sum(x => x.Quantity)
            };
        }
        public static Expression<Func<Product,ProductDTO>> ToProductDtoExpression()
        {
            return product => new ProductDTO()
            {
                Name = product.Name,
                InStorageCount = product.ProductStocks
                    .Sum(x => x.Quantity)
            };
        }
    }
}
