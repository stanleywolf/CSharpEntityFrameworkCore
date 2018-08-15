using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using P01.ProductShop.Data;
using P01.ProductShop.ExportDtos;
using P01.ProductShop.Services;
using P01.ProductShop.Services.Entity;
using DataAnotations = System.ComponentModel.DataAnnotations;

namespace P01.ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<ProductShopProfile>();
            //});

            //var mapper = config.CreateMapper();

            #region users

            //var xmlString = File.ReadAllText("Xmls/users.xml");

            //var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));

            //var deserializedUsers = (UserDto[])serializer.Deserialize(new StringReader(xmlString));

            //List<User> users = new List<User>();

            //foreach (var userDto in deserializedUsers)
            //{
            //    if (!IsValid(userDto))
            //    {
            //        continue;
            //    }

            //    var user = mapper.Map<User>(userDto);

            //    users.Add(user);
            //}
            //var context = new ProductShopDbContext();
            //context.AddRange(users);
            //context.SaveChanges();

            #endregion

            #region products

            //var xmlString = File.ReadAllText("Xmls/products.xml");

            //var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));

            //var deserializedUsers = (ProductDto[])serializer.Deserialize(new StringReader(xmlString));

            //List<Product> products = new List<Product>();

            //var countBuyer = 1;

            //foreach (var productDto in deserializedUsers)
            //{
            //    if (!IsValid(productDto))
            //    {
            //        continue;
            //    }

            //    var product = mapper.Map<Product>(productDto);

            //    var buyerId = new Random().Next(1, 56);
            //    var sellerId = new Random().Next(1, 56);

            //    product.SellerId = sellerId;
            //    product.BuyerId = buyerId;

            //    if (countBuyer == 4)
            //    {
            //        product.BuyerId = null;
            //        countBuyer = 1;
            //    }

            //    products.Add(product);
            //    countBuyer++;
            //}
            //var context = new ProductShopDbContext();
            //context.AddRange(products);
            //context.SaveChanges();

            #endregion

            #region categoryProducts

            //List<CategoryProduct> categoryProducts = new List<CategoryProduct>();
            //for (int productId = 1; productId <= 200; productId++)
            //{
            //    var categoryId = new Random().Next(26,37);
            //    var categoryProduct = new CategoryProduct()
            //    {
            //        ProductId = productId,
            //        CategoryId = categoryId
            //    };

            //    categoryProducts.Add(categoryProduct);
            //}
            //var context = new ProductShopDbContext();
            //context.CategoryProducts.AddRange(categoryProducts);
            //context.SaveChanges();

            #endregion

            #region categories

            //var xmlString = File.ReadAllText("Xmls/categories.xml");

            //var serializer = new XmlSerializer(typeof(CategoryDto[]),new XmlRootAttribute("categories"));
            //var deserializeCategories = (CategoryDto[])serializer.Deserialize(new StringReader(xmlString));

            //List<Category> categories = new List<Category>();

            //foreach (var categoryDto in deserializeCategories)
            //{
            //    if (!IsValid(categoryDto))
            //    {
            //        continue;
            //    }
            //    var category = mapper.Map<Category>(categoryDto);

            //    categories.Add(category);
            //}
            //var context = new ProductShopDbContext();
            //context.Categories.AddRange(categories);
            //context.SaveChanges();


            #endregion

            #region exportProductInRange

            //var products = context.Products
            //    .Where(x => x.Price >= 1000 && x.Price <= 2000 && x.Buyer != null)
            //    .OrderByDescending(p => p.Price)
            //    .Select(x => new ProductDto()
            //    {
            //        Name = x.Name,
            //        Price = x.Price,
            //        Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName ?? x.Buyer.LastName
            //    })
            //    .ToArray();
            //var sb = new StringBuilder();

            //var serializer = new XmlSerializer(typeof(ProductDto[]),new XmlRootAttribute("products"));

            //var xmlNamespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});

            //serializer.Serialize(new StringWriter(sb),products,xmlNamespaces);

            //File.WriteAllText("Xmls/products-in-range.xml",sb.ToString());

            #endregion

            #region usersSoldProduct

            //var context = new ProductShopDbContext();

            //var users = context.Users
            //    .Where(s => s.ProductsSold.Count >= 1)
            //    .OrderBy(x => x.LastName)
            //    .ThenBy(x => x.FirstName)
            //    .Select(x => new UserDto()
            //    {
            //        FirstName = x.FirstName,
            //        LastName = x.LastName,
            //        SoldProduct = x.ProductsSold
            //            .Select(p => new SoldProductDto()
            //            {
            //                Name = p.Name,
            //                Price = p.Price
            //            }).ToArray()
            //    })
            //    .ToArray();

            //var sb = new StringBuilder();

            //var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));

            //var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            //serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);

            //File.WriteAllText("Xmls/users-sold-product.xml", sb.ToString());


            #endregion

            #region categoriesbyproduct

            //var context = new ProductShopDbContext();

            //var categories = context.Categories
            //    .OrderByDescending(s => s.CategoryProducts.Count)
            //    .Select(x => new CategoryDto()
            //    {
            //        Name = x.Name,
            //        Count = x.CategoryProducts.Count,
            //        TotalRevenue = x.CategoryProducts.Sum(s => s.Product.Price),
            //        AveragePrice = x.CategoryProducts.Select(s => s.Product.Price).DefaultIfEmpty().Average()
            //    })
            //    .ToArray();


            //var sb = new StringBuilder();

            //var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));

            //var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            //serializer.Serialize(new StringWriter(sb), categories, xmlNamespaces);

            //File.WriteAllText("Xmls/categories-by-products.xml", sb.ToString());

            #endregion
            var context = new ProductShopDbContext();

            var users = new UserDtoTwo()
            {
                Count = context.Users.Count(),
                User =  context.Users
                .Where(x => x.ProductsSold.Count >= 1)
                .Select(x => new UserDtoTwoTwo()
                {
                    Firstname = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age.ToString(),
                    SoldProducts = new SoldProduct
                    {
                        Count = x.ProductsSold.Count(),
                        ProductDtos = x.ProductsSold.Select(k => new ProductDtoTwo()
                        {
                            Name = k.Name,
                            Price = k.Price
                        }).ToArray()
                    }
                }).ToArray()
            };
                
                

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(UserDtoTwo), new XmlRootAttribute("users"));

            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);

            File.WriteAllText("Xmls/users-and-products.xml", sb.ToString());


        }

        public static bool IsValid(object obj)
        {
            var validationContext = new DataAnotations.ValidationContext(obj);

            var validationResult = new List<DataAnotations.ValidationResult>();

            return DataAnotations.Validator.TryValidateObject(obj, validationContext, validationResult);
        }
    }
}
