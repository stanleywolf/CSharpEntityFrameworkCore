using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProductShop.App
{
    using AutoMapper;

    using Data;
    using Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            var mapper = config.CreateMapper();

            var context = new ProductShopContext();

           // DeserializeUsers(context);
            //DeserializeProducts(context);
            //DeserializeCategories(context);
            //DeserializeCategoryProducts(context);

            //QueryOneProductInRange(context);
           // QueryTwoSuccessfullySoldProduct(context);
            //QueryThreeCategoryByProductCount(context);
            QueryFourUsersAndProducts(context);

        }

        private static void QueryFourUsersAndProducts(ProductShopContext context)
        {
            var users = new
            {
                usersCount = context.Users.Count(),
                users = context.Users
                    .OrderByDescending(x => x.ProductsSold.Count)
                    .ThenBy(x => x.LastName)
                    .Where(x => x.ProductsSold.Count >= 1 && x.ProductsSold.Any(z => z.Buyer != null))
                    .Select(x => new
                    {
                        firstName = x.FirstName,
                        lastName = x.LastName,
                        age = x.Age,
                        soldProduct = new
                        {
                            count = x.ProductsSold.Count,
                            products = x.ProductsSold.Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            })
                            .ToArray()
                        }
                    })
                    .ToArray()
            };


            var jsonproducts = JsonConvert.SerializeObject(users, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("Json/users-and-products.json", jsonproducts);
        }

        private static void QueryThreeCategoryByProductCount(ProductShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(x => x.CategoryProducts.Count)
                .Select(x => new
                {
                    category = x.Name,
                    productCount = x.CategoryProducts.Count,
                    averagePrice = x.CategoryProducts.Sum(p => p.Product.Price) / x.CategoryProducts.Count,
                    totalRevenue = x.CategoryProducts.Sum(c => c.Product.Price)
                })
                .ToArray();

            var jsonproducts = JsonConvert.SerializeObject(categories, Formatting.Indented);

            File.WriteAllText("Json/categories-by-products.json", jsonproducts);
        }

        private static void QueryTwoSuccessfullySoldProduct(ProductShopContext context)
        {
            var users = context.Users
                .Where(x => x.ProductsSold.Count >= 1 && x.ProductsSold.Any(c => c.Buyer != null))
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    soldProduct = x.ProductsSold
                    .Where(z => z.Buyer != null)
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Buyer.FirstName,
                        buyerLastName = p.Buyer.LastName,
                    })
                    .ToArray()
                })
                .ToArray();

            var jsonproducts = JsonConvert.SerializeObject(users,new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("Json/users-sold-products.json", jsonproducts);
        }

        private static void QueryOneProductInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .OrderBy(x => x.Price)
                .Select(x => new
                {
                    name = x.Name,
                    price = x.Price,
                    seller = x.Seller.FirstName + " " + x.Seller.LastName ?? x.Seller.LastName
                })
                .ToArray();

            var jsonproducts = JsonConvert.SerializeObject(products,Formatting.Indented);

            File.WriteAllText("Json/products-in-range.json", jsonproducts);
        }

        private static void DeserializeCategoryProducts(ProductShopContext context)
        {
            List<CategoryProduct> categoruProducts = new List<CategoryProduct>();
            for (int i = 1; i <= 200; i++)
            {
                var categotyId = new Random().Next(1,12);

                var categoryProduct = new CategoryProduct()
                {
                    CategoryId = categotyId,
                    ProductId = i
                };
                categoruProducts.Add(categoryProduct);
            }
            context.CategoryProducts.AddRange(categoruProducts);
            context.SaveChanges();
        }

        private static void DeserializeCategories(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("Json/categories.json");

            var deserializedCategories = JsonConvert.DeserializeObject<Category[]>(jsonString);

            List<Category> categories = new List<Category>();

            foreach (var category in deserializedCategories)
            {
                if (IsValid(category))
                {
                    categories.Add(category);
                }
            }
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void DeserializeProducts(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("Json/products.json");

            var deserializedProducts = JsonConvert.DeserializeObject<Product[]>(jsonString);

            List<Product> products = new List<Product>();

            foreach (var product in deserializedProducts)
            {
                if (IsValid(product))
                {
                    var sellerId = new Random().Next(1,35);
                    var buyerId = new Random().Next(35,57);

                    var random = new Random().Next(1,4);

                    product.SellerId = sellerId;
                    product.BuyerId = buyerId;

                    if (random == 3)
                    {
                        product.BuyerId = null;
                    }
                    products.Add(product);
                }
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void DeserializeUsers(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("Json/users.json");

            var deserializedUsers = JsonConvert.DeserializeObject<User[]>(jsonString);

            List<User> users = new List<User>();
            foreach (var user in deserializedUsers)
            {
                if (IsValid(user))
                {
                 users.Add(user);
                }
            }
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);

            var result = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, result, true);
        }
    }
}
