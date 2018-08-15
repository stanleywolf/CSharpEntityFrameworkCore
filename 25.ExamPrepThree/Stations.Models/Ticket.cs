namespace Stations.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Ticket
    {
        public int Id { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required, RegularExpression("[A-Z]{2}[0-9]{1,6}")]
        public string SeatingPlace { get; set; }

        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        public int? CustomerCardId { get; set; }

        public virtual CustomerCard CustomerCard { get; set; }
    }
}
