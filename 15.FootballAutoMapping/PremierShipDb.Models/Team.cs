using System;
using System.ComponentModel.DataAnnotations;

namespace PremierShipDb.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Stadium { get; set; }

        [Required]
        public decimal Budget { get; set; }

        [Required]
        public double WorldRanking { get; set; }

        public DateTime DateFounded { get; set; }

        public string AddressTown { get; set; }


    }
}
