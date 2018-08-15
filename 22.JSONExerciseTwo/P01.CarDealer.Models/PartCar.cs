using System;
using System.Collections.Generic;
using System.Text;

namespace P01.CarDealer.Models
{
    public class PartCar
    {
        public int Part_Id { get; set; }
        public virtual Part Part { get; set; }

        public int Car_Id { get; set; }
        public virtual Car Car { get; set; }
    }
}
