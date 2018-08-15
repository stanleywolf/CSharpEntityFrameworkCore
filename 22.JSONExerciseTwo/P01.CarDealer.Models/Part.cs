using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01.CarDealer.Models
{
    public class Part
    {
        public Part()
        {
            this.PartCars = new List<PartCar>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal  Price { get; set; }

        public int Quantity { get; set; }

        public int Supplier_Id { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<PartCar> PartCars { get; set; }

    }
}
