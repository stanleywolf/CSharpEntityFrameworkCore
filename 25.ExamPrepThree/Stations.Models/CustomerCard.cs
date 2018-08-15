namespace Stations.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public enum CardType
    {
        Normal,
        Pupil,
        Student,
        Elder,
        Debilitated
    }

    public class CustomerCard
    {
        public CustomerCard()
        {
            this.BoughtTickets = new List<Ticket>();
        }

        public int Id { get; set; }
        
        [StringLength(128)]
        [Required]
        public string Name { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        public CardType Type { get; set; }

        public virtual ICollection<Ticket> BoughtTickets { get; set; }
    }
}
