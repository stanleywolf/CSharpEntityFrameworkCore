using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using FastFood.Models.Enums;

namespace FastFood.Models
{
    public class Order
    {
    //•	Id – integer, Primary Key
    //•	Customer – text(required)
    //•	DateTime – date and time of the order(required)
    //•	Type – OrderType enumeration with possible values: “ForHere, ToGo(default: ForHere)” (required)
    //•	TotalPrice – decimal value(calculated property, (not mapped to database), required)
    //•	EmployeeId – integer, foreign key
    //•	Employee – The employee who will process the order(required)
    //•	OrderItems – collection of type OrderItem

        public Order()
        {
            this.OrderItems=new List<OrderItem>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Customer { get; set; }

        [Required]
        public DateTime  DateTime { get; set; }

        [Required]
        public OrderType Type { get; set; } = OrderType.ForHere;

        [NotMapped]
        [Required]
        public decimal TotalPrice => this.OrderItems.Sum(x => x.Item.Price * x.Quantity);

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
