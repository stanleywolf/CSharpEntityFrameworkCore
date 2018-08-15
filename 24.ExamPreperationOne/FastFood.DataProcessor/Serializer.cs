using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Export;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace FastFood.DataProcessor
{
	public class Serializer
	{
	    public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
	    {


	        var employee = context.Employees
                .ToArray()
	            .Where(x => x.Name == employeeName)
	            .Select(x => new
	            {
	                Name = x.Name,
	                Orders = x.Orders
                            .Where(s => s.Type.ToString() == orderType)
                            .Select(o => new
	                    {
	                        Customer = o.Customer,
                            Items = o.OrderItems.Select(oi => new
                            {
                                Name = oi.Item.Name,
                                Price = oi.Item.Price,
                                Quantity = oi.Quantity
                            })
                            .ToArray(),
                            TotalPrice = o.TotalPrice
	                    })
                        .OrderByDescending(t => t.TotalPrice)
                        .ThenByDescending(i => i.Items.Length)
                        .ToArray(),
                     TotalMade = x.Orders.Where(s => s.Type.ToString() == orderType).Sum(e => e.TotalPrice)
	            })
                .FirstOrDefault();

	        var jsonString = JsonConvert.SerializeObject(employee, Formatting.Indented);

	        return jsonString;
	    }

	    public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
	    {
	        var categoriesArray = categoriesString.Split(",");

	        var caregories = context.Categories
	            .Where(x => categoriesArray.Any(s => s == x.Name))
	            .Select(x => new CategoryDto
	            {
	                Name = x.Name,
	                MostPopularItem = x.Items.Select(z => new MostPopularItemDto()
	                    {
	                        Name = z.Name,
	                        TotalMade = z.OrderItems.Sum(c => c.Item.Price * c.Quantity),
	                        TimeSold = z.OrderItems.Sum(p => p.Quantity)
	                    })
	                    .OrderByDescending(v => v.TotalMade)
	                    .ThenByDescending(v => v.TimeSold)
	                    .FirstOrDefault()
	            })
                .OrderByDescending(x => x.MostPopularItem.TotalMade)
                .ThenByDescending(x => x.MostPopularItem.TimeSold)
                .ToArray();

	        var sb = new StringBuilder();

            var xmlNamespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});
             var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("Categories"));

            serializer.Serialize(new StringWriter(sb),caregories,xmlNamespaces );

	        return sb.ToString();
	    }
	}
}