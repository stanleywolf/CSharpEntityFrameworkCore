namespace Stations.Dto.Export
{
    using System.Collections.Generic;

    using Stations.Models;

    public class CardDto
    {
        public CardDto()
        {
            this.Tickets = new List<CardTicketDto>();
        }

        public string Name { get; set; }

        public CardType Type { get; set; }

        public ICollection<CardTicketDto> Tickets { get; set; }
    }
}
