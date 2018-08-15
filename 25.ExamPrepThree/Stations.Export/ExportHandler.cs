namespace Stations.Export
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Newtonsoft.Json;

    using Stations.Data;
    using Stations.Dto.Export;
    using Stations.Models;

    public class ExportHandler
    {
        private readonly QueryHelper queryHelper;

        public ExportHandler(QueryHelper queryHelper)
        {
            this.queryHelper = queryHelper;

            Mapper.Initialize(
                cfg =>
                    {
                        cfg.CreateMap<IGrouping<string, TripDto>, TrainDto>()
                            .ForMember(dest => dest.TrainNumber, mo => mo.MapFrom(src => src.Key))
                            .ForMember(dest => dest.DelayedTimes, mo => mo.MapFrom(src => src.Count()))
                            .ForMember(dest => dest.MaxDelayedTime, mo => mo.MapFrom(src => GetHighestTime(src)));

                        cfg.CreateMap<Ticket, TicketDto>()
                            .ForMember(dest => dest.OriginStation, mo => mo.MapFrom(src => src.Trip.OriginStation.Name))
                            .ForMember(dest => dest.DestinationStation, mo => mo.MapFrom(src => src.Trip.DestinationStation.Name))
                            .ForMember(dest => dest.DepartureTime, mo => mo.MapFrom(src => src.Trip.DepartureTime))
                            .ForMember(dest => dest.ArrivalTime, mo => mo.MapFrom(src => src.Trip.ArrivalTime));

                        cfg.CreateMap<Ticket, CardTicketDto>()
                            .ForMember(dest => dest.OriginStation, mo => mo.MapFrom(src => src.Trip.OriginStation.Name))
                            .ForMember(dest => dest.DestinationStation, mo => mo.MapFrom(src => src.Trip.DestinationStation.Name))
                            .ForMember(dest => dest.DepartureTime, mo => mo.MapFrom(src => src.Trip.DepartureTime));

                        cfg.CreateMap<CustomerCard, CardDto>()
                        .ForMember(dest => dest.Tickets, mo => mo.MapFrom(src => src.BoughtTickets));
                    });
        }

        public void ExportDelayedTrains(string path, string dateAsString)
        {
            DateTime date = ParseDate(dateAsString);
            var delayedTrips = this.queryHelper
            .Filter<Trip>(trip => trip.Status == TripStatus.Delayed && trip.DepartureTime <= date)
            .Select(
                trip =>
                        new TripDto
                        {
                            TrainNumber = trip.Train.TrainNumber,
                            TimeDifference = trip.TimeDifference
                        })
            .GroupBy(trip => trip.TrainNumber)
            .ToList()
            .AsQueryable()
            .ProjectTo<TrainDto>()
            .OrderByDescending(t => t.DelayedTimes)
            .ThenByDescending(t => t.MaxDelayedTime)
            .ThenBy(t => t.TrainNumber);

            string json = JsonConvert.SerializeObject(
                delayedTrips,
                Formatting.Indented);

            File.WriteAllText($"{path}delayed-trips-by-{date.Date:dd-MM-yyyy}.json", json);
        }

        public void ExportCardsTicket(
            string path,
            string cardType)
        {
            XDocument document = new XDocument();
            CardType type = (CardType)Enum.Parse(typeof(CardType), cardType);

            var cards = this.queryHelper.Filter<CustomerCard>(c => c.Type == type)
                .ProjectTo<CardDto>()
                .Where(c => c.Tickets.Count > 0)
                .OrderBy(c => c.Name);

            XElement root = new XElement("Cards");
            foreach (CardDto cardDto in cards)
            {
                XElement cardElement = new XElement("Card");
                cardElement.SetAttributeValue("name", cardDto.Name);
                cardElement.SetAttributeValue("type", cardDto.Type.ToString());

                XElement tickets = new XElement("Tickets");
                foreach (CardTicketDto ticketDto in cardDto.Tickets)
                {
                    XElement ticketElement = new XElement("Ticket");
                    ticketElement.SetElementValue("OriginStation", ticketDto.OriginStation);
                    ticketElement.SetElementValue("DestinationStation", ticketDto.DestinationStation);
                    ticketElement.SetElementValue("DepartureTime", ticketDto.DepartureTime.ToString("dd/MM/yyyy HH:mm"));

                    tickets.Add(ticketElement);
                }

                cardElement.Add(tickets);
                root.Add(cardElement);
            }

            document.Add(root);
            document.Save($"{path}tickets-bought-withCardType-{cardType}.xml");
        }

        private static TimeSpan GetHighestTime(IGrouping<string, TripDto> grouping)
        {
            TimeSpan result = grouping
                .Where(trip => trip.TimeDifference.HasValue)
                .OrderByDescending(trip => trip.TimeDifference)
                .Select(trip => trip.TimeDifference.Value)
                .FirstOrDefault();

            return result;
        }

        private static DateTime ParseDate(string dateAsString)
        {
            return DateTime.ParseExact(dateAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }
}
