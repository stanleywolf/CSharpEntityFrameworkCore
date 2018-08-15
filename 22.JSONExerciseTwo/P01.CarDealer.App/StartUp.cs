using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using P01.CarDealer.Data;
using P01.CarDealer.Models;

namespace P01.CarDealer.App
{
   public  class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();

            //InitializeSuppliers(context);
            //InitializeParts(context);
            //InitializeCars(context);
            //InitializeCustomers(context);
            //InitializeCarParts(context);
            //InitializeSalesPerCarAndCustomer(context);

            //QueryOrderedCustomers(context);
            //QueryCarsToyota(context);
            //QueryLocalSuppliers(context);
            //QueryCarsAndTheirListOfParts(context);
            //QueryTotalSalePerCustomer(context);
            QuerySalesWithDiscount(context);

           
        }

        private static void QuerySalesWithDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(x => new
                {
                    Car = new
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TravelledDistance = x.Car.TravelledDistance
                    },
                    customerName = x.Customer.Name,
                    Discount = (decimal)x.Discount / 100,
                    Price = x.Car.PartCars.Sum(z => z.Part.Price) ,
                    priceWithDiscount = x.Car.PartCars.Sum(z => z.Part.Price) *((100 - (decimal)x.Discount) / 100),

                })
                .ToArray();

            var jsonCars = JsonConvert.SerializeObject(sales, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("Json/sales-discounts.json.json", jsonCars);
        }

        private static void QueryTotalSalePerCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(x => x.Cars.Count >= 1)
                .Select(x => new
                {
                    fullName = x.Name,
                    boughtCars = x.Cars.Count,
                    spentMoney = x.Cars.Sum(p => p.PartCars.Sum(z => z.Part.Price))
                })
                .OrderByDescending(x => x.spentMoney)
                .ThenByDescending(x => x.boughtCars)
                .ToArray();

                var jsonCars = JsonConvert.SerializeObject(customers, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("Json/customers-total-sales.json", jsonCars);
        }

        private static void QueryCarsAndTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(x => new
                {
                    Car = new
                    {
                        Make = x.Make,
                        Model = x.Model,
                        TravelledDistance = x.TravelledDistance,
                    },
                    Parts = new
                    {
                        Name = x.PartCars.Select(p => new
                            {
                                Name = p.Part.Name,
                                Price = p.Part.Price
                            })
                            .ToArray()
                    },
                })
                .ToArray();

            var jsonCars = JsonConvert.SerializeObject(cars, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("Json/cars-and-parts.json", jsonCars);
        }

        private static void QueryLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => x.IsImported == false)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToArray();

            var jsonSupplier = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            File.WriteAllText("Json/local-suppliers.json",jsonSupplier);
        }

        private static void QueryCarsToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(x => x.Make == "Toyota")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .Select(x => new
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .ToArray();

            var jsonCars = JsonConvert.SerializeObject(cars, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("Json/toyota-cars.json",jsonCars);
        }

        private static void QueryOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(x => x.BirthDate)
                .ThenBy(x => x.IsYoungDriver)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    IsYoungDriver = x.IsYoungDriver,
                    Sales = new Sale[0],
                })
                .ToArray();
            var jsonProduct = JsonConvert.SerializeObject(customers, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("Json/ordered-customers.json",jsonProduct);

        }

        private static void InitializeSalesPerCarAndCustomer(CarDealerContext context)
        {
            Car[] cars = context.Cars.ToArray();
            Customer[] customers = context.Customers.ToArray();

            var random = new Random();
            List<Sale> sales = new List<Sale>();

            
            for (int i = 1; i < cars.Length; i++)
            {
                Customer customer = customers[random.Next(customers.Length - 1)];

                int discount = GetDiscount(customer.IsYoungDriver);

                var sale = new Sale()
                {
                    Customer = customer,
                    Car = cars[i],
                    
                    Discount = discount
                };
                sales.Add(sale);
            }
            context.Sales.AddRange(sales);
            context.SaveChanges();
        }

        private static int GetDiscount(bool customerIsYoungDriver)
        {
            var discounts = new int[] {0, 5, 10, 20, 30, 40, 50};
            int discount = discounts[new Random().Next(0, discounts.Length)];

            if (customerIsYoungDriver)
            {
                discount += 5;
            }
            return discount;
        }

        private static void InitializeCarParts(CarDealerContext context)
        {
            Car[] cars = context.Cars.ToArray();
            Part[] parts = context.Parts.ToArray();

            Random random = new Random();
            foreach (Car car in cars)
            {
                car.PartCars = GeneratePartCars(parts, random.Next(10, 20));
            }
            
            context.SaveChanges();
        }

        private static ICollection<PartCar> GeneratePartCars(ICollection<Part> parts, int count)
        {
            var rangeOfParts = new List<Part>();
            Random random = new Random();

            while (rangeOfParts.Count < count)
            {
                rangeOfParts.Add(parts.ElementAt(random.Next(0, parts.Count - 1)));

                if (rangeOfParts.Count == count)
                {
                    rangeOfParts = rangeOfParts.Distinct().ToList();
                }
            }

            var partCars = new List<PartCar>();
            foreach (var part in rangeOfParts)
            {
                partCars.Add(new PartCar
                {
                    Part = part
                });
            }

            return partCars;
        }

        private static void InitializeCustomers(CarDealerContext context)
        {
            var jsonString = File.ReadAllText("Json/customers.json");
            var deserializeCustomers = JsonConvert.DeserializeObject<Customer[]>(jsonString);

            List<Customer> customers = new List<Customer>();
            foreach (var customer in deserializeCustomers)
            {
                if (IsValid(customer))
                {
                    customers.Add(new Customer()
                    {
                       Name = customer.Name,
                       BirthDate = customer.BirthDate,
                       IsYoungDriver = customer.IsYoungDriver
                    });
                }
            }
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void InitializeCars(CarDealerContext context)
        {
            var jsonString = File.ReadAllText("Json/cars.json");
            var deserializeCars = JsonConvert.DeserializeObject<Car[]>(jsonString);

            List<Car> cars = new List<Car>();
            foreach (var car in deserializeCars)
            {
                if (IsValid(car))
                {
                    cars.Add(new Car()
                    {
                        Make = car.Make,
                        Model = car.Model,
                        TravelledDistance = car.TravelledDistance,
                        
                    });
                }
            }
            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void InitializeParts(CarDealerContext context)
        {
            var jsonString = File.ReadAllText("Json/parts.json");

            var deserializeParts = JsonConvert.DeserializeObject<Part[]>(jsonString);

            List<Part> parts = new List<Part>();
            foreach (var part in deserializeParts)
            {
                if (IsValid(part))
                {
                    var supplierId = new Random().Next(1,32);
                   parts.Add(new Part()
                    {
                        Name = part.Name,
                        Price = part.Price,
                        Quantity = part.Quantity,
                        Supplier_Id = supplierId
                    });

                }
            }
            context.Parts.AddRange(parts);
            context.SaveChanges();
        }

        private static void InitializeSuppliers(CarDealerContext context)
        {
            var jsonString = File.ReadAllText("Json/suppliers.json");

            var deserializeSuppliers = JsonConvert.DeserializeObject<Supplier[]>(jsonString);

            List<Supplier> suppliers = new List<Supplier>();

            foreach (var supplier in deserializeSuppliers)
            {
                if (IsValid(supplier))
                {
                    suppliers.Add(supplier);
                }
            }
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }
        public static bool IsValid(object obj)
        {
            var validationContext =new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var result = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, result, true);
        }
    }
    
}
