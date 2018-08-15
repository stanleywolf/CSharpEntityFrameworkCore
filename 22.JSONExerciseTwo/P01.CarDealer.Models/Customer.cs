using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01.CarDealer.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Cars = new List<Car>();
            this.Sales = new List<Sale>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }

        public virtual  ICollection<Car> Cars { get; set; }
        
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
