using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01.CarDealer.Models
{
   public  class Sale
    {
        [Key]
        public int Id { get; set; }

        public int Discount { get; set; }

        public int Car_Id { get; set; }
        public virtual Car Car { get; set; }

        public int Customer_Id { get; set; }
        public virtual Customer Customer { get; set; }


    }
}
