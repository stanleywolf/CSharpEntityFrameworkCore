using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;
using FastFood.Models.Enums;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";

		public static string ImportEmployees(FastFoodDbContext context, string jsonString)
		{
		    var deserializeEmployee = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);

		    var sb = new StringBuilder();

            List<Employee> employees = new List<Employee>();
		    foreach (var employeeDto in deserializeEmployee)
		    {
		        if (!IsValid(employeeDto))
		        {
		            sb.AppendLine(FailureMessage);
                    continue;
		        }
		        var position = context.Positions.FirstOrDefault(x => x.Name == employeeDto.Position);
		        if (position == null)
		        {
		            position = CreatePosition(context,employeeDto);
		        }
                Employee employee = new Employee()
                {
                    Name = employeeDto.Name,
                    Age = employeeDto.Age,
                    Position = position
                };
                employees.Add(employee);
		        sb.AppendLine(String.Format(SuccessMessage, employee.Name));
		    }
            context.Employees.AddRange(employees);
		    context.SaveChanges();

		    return sb.ToString().TrimEnd();
		}

	    public static string ImportItems(FastFoodDbContext context, string jsonString)
		{
		    var deserializeItems = JsonConvert.DeserializeObject<ItemsDto[]>(jsonString);

		    var sb = new StringBuilder();

            List<Item> items = new List<Item>();

		    foreach (var itemDto in deserializeItems)
		    {
		        if (!IsValid(itemDto))
		        {
		            sb.AppendLine(FailureMessage);
                    continue;
		        }

		        bool itemExist = items.Any(x => x.Name == itemDto.Name);

		        if (itemExist)
		        {
		            sb.AppendLine(FailureMessage);
                    continue;
		        }
		        var caregory = context.Categories.FirstOrDefault(x => x.Name == itemDto.Category);

		        if (caregory == null)
		        {
		            caregory = CreateCategory(context, itemDto.Category);
		        }

                Item item = new Item()
                {
                    Name = itemDto.Name,
                    Price = itemDto.Price,
                    Category = caregory
                };
                items.Add(item);

		        sb.AppendLine(String.Format(SuccessMessage, item.Name));
		    }

            context.Items.AddRange(items);
		    context.SaveChanges();

		    return sb.ToString().TrimEnd();
		}

	    public static string ImportOrders(FastFoodDbContext context, string xmlString)
	    {
	        var serializer = new XmlSerializer(typeof(OrderDto[]), new XmlRootAttribute("Orders"));

	        var deserOrders = (OrderDto[])serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            List<OrderItem> orderItems = new List<OrderItem>();
            List<Order> orders = new List<Order>();
	        foreach (OrderDto orderDto in deserOrders)
	        {
	            bool isValidItem = true;

	            if (!IsValid(orderDto))
	            {
	                sb.AppendLine(FailureMessage);
                    continue;
	            }
	            foreach (var itemsDto in orderDto.OrderItemsDto)
	            {
	                if (!IsValid(itemsDto))
	                {
	                    sb.AppendLine(FailureMessage);
	                    isValidItem = false;
	                    break;
                    }
	            }
	            if (isValidItem == false)
	            {
	                sb.AppendLine(FailureMessage);
	                continue;
                }
	            var employee = context.Employees.FirstOrDefault(x => x.Name == orderDto.Employee);

	            if (employee == null)
	            {
	                sb.AppendLine(FailureMessage);
	                continue;
                }

	            var areValidItems = AreValidItems(context, orderDto.OrderItemsDto);

	            if (!areValidItems)
	            {
	                sb.AppendLine(FailureMessage);
	                continue;
                }
	            var date = DateTime.ParseExact(orderDto.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

	            var orderType = Enum.Parse<OrderType>(orderDto.Type);

                var order = new Order()
                {
                    Customer = orderDto.Customer,
                    Employee = employee,
                    DateTime = date,
                    Type = orderType,

                };

	            orders.Add(order);

	            foreach (var itemDto in orderDto.OrderItemsDto)
	            {
	                var item = context.Items.FirstOrDefault(x => x.Name == itemDto.Name);

	                var orderItem = new OrderItem()
	                {
	                    Order = order,
                        Item = item,
                        Quantity = itemDto.Quantity
	                };

                    orderItems.Add(orderItem);
	            }
	            sb.AppendLine($"Order for {orderDto.Customer} on {date.ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture)} added");
	        }
            context.Orders.AddRange(orders);
	        context.SaveChanges();

            context.OrderItems.AddRange(orderItems);
	        context.SaveChanges();

	        return sb.ToString().TrimEnd();
	    }

	    private static bool AreValidItems(FastFoodDbContext context, OrderItemsDto[] orderDtoOrderItemsDto)
	    {
	        foreach (var item in orderDtoOrderItemsDto)
	        {
	            var itemExist = context.Items.Any(x => x.Name == item.Name);
	            if (!itemExist)
	            {
	                return false;
	            }
	        }
	        return true;
	    }


	    private static bool IsValid(object obj)
	    {
	        var validateContext = new ValidationContext(obj);
            var validResult = new List<ValidationResult>();

	        return Validator.TryValidateObject(obj, validateContext, validResult, true);
	    }

	    private static Position CreatePosition(FastFoodDbContext context,EmployeeDto employeeDto)
	    {
	        Position position = new Position()
	        {
	            Name = employeeDto.Position
	        };

	        context.Positions.Add(position);
	        context.SaveChanges();

	        return position;
	    }

	    private static Category CreateCategory(FastFoodDbContext context, string itemDtoCategory)
	    {
            Category category = new Category()
            {
                Name = itemDtoCategory
            };

	        context.Categories.Add(category);
	        context.SaveChanges();

	        return category;
	    }
    }
}