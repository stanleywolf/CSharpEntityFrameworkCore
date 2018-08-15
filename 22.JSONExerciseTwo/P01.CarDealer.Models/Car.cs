using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01.CarDealer.Models
{
    public class Car
    {
        public Car()
        {
            this.PartCars = new List<PartCar>();
            this.Sales = new List<Sale>();
        }
        [Key]
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public virtual ICollection<PartCar> PartCars { get; set; }
        
        public virtual  ICollection<Sale> Sales { get; set; }
    }
}
