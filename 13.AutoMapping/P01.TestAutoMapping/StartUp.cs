using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using P01.TestAutoMapping.Data;
using P01.TestAutoMapping.Data.Models;
using P01.TestAutoMapping.Data.Models.Mapping;

namespace P01.TestAutoMapping
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new AutoMappingDB();

            #region Seed
            //string[] productName = new string[] { "Door", "Comp", "Hard", "Disc", "Mouse" };
            //string[] prodDesc = new string[] { "Waww", "Maliiii", "Boje", "OMG" };
            //string[] storageNames = new string[] { "Kafcha", "Golemakashta", "Warehouse", "SmallHouse" };
            //string[] storageLocat = new string[] { "Sofia", "Rayovo", "Samokov", "Prodanovci" };


            //List<Storage> storages = new List<Storage>();
            //Random rand = new Random();
            //for (int i = 0; i < 3; i++)
            //{

            //    int locIndex = rand.Next(0, 5);

            //    var storage = new Storage()
            //    {
            //        Name = storageNames[i],
            //        Location = storageLocat[locIndex]
            //    };
            //    storages.Add(storage);
            //}
            //context.Storages.AddRange(storages);
            //context.SaveChanges();

            //for (int i = 0; i < productName.Length; i++)
            //{
            //    int descIndex = rand.Next(0, 3);
            //    int storageIndex = rand.Next(0, 3);

            //    var entry = new Product()
            //    {
            //        Name = productName[i],
            //        Description = prodDesc[descIndex],
            //        ProductStocks = new List<ProductStock>()
            //        {
            //            new ProductStock()
            //            {
            //                Quantity = rand.Next(0, 101),
            //                Storage = storages.ElementAt(storageIndex)
            //            }
            //        }
            //    };
            //    context.Products.Add(entry);
            //}
            //context.SaveChanges();
                #endregion
            Mapper.Initialize(cfg =>
            {
               cfg.AddProfile<ProductProfile>();
               cfg.AddProfile<StorageProfile>();
            });

            Product entity = context.Products.First();

            ProductDTO dto = Mapper.Map<ProductDTO>(entity);

           //CollectionMapping
            List<ProductDTO> autoDtos = context.Products
                .ProjectTo<ProductDTO>()
                .ToList();
                

            //using Expression in select
            List<ProductDTO> dtos = context.Products
                .Select(ProductMapping.ToProductDtoExpression())
               .ToList();
        }

        
    }
}
