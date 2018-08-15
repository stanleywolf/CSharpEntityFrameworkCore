namespace Stations.Dto.Export
{
    using System;

    public class CardTicketDto
    {
        public string Seat { get; set; }

        public decimal Price { get; set; }

        public string OriginStation { get; set; }

        public string DestinationStation { get; set; }

        public DateTime DepartureTime { get; set; }
    }
}
