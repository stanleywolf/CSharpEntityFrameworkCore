using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _01.CodeFirst.Models
{
    public class KeyCard
    {
        [Key]
        public int Id { get; set; }

        public int CardNumber { get; set; }

       
        public ICollection<RoomsKeyCards> RoomsKeyCards { get; set; }
    }
}
