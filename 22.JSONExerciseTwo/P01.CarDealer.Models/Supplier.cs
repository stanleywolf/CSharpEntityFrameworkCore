using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01.CarDealer.Models
{
    public class Supplier
    {

        public Supplier()
        {
            this.Parts = new List<Part>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsImported { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
    }
}
