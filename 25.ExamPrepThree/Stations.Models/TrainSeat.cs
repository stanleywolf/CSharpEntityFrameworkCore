namespace Stations.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TrainSeat
    {
        public int Id { get; set; }

        public int TrainId { get; set; }

        public virtual Train Train { get; set; }

        public int SeatingClassId { get; set; }

        public virtual SeatingClass SeatingClass { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
