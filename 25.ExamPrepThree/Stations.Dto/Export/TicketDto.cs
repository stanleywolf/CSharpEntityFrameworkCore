namespace Stations.Dto.Export
{
    using System;

    public class TicketDto
    {
        public decimal Price { get; set; }

        public string OriginStation { get; set; }

        public string DestinationStation { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }
    }
}
