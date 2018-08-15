namespace Stations.Import
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using Stations.Data;
    using Stations.Dto.Import;
    using Stations.Models;

    public class ImportHandler
    {
        private const string ErrorMessage = "Invalid data format.";

        private const string RecordSuccessfullyImported = "Record {0} successfully imported.";

        private readonly QueryHelper queryHelper;

        public ImportHandler(QueryHelper queryHelper)
        {
            this.queryHelper = queryHelper;

            Mapper.Initialize(
                cfg =>
                    {
                        cfg.CreateMap<SeatsClassPerTrainDto, SeatingClass>();
                        cfg.CreateMap<TrainCreateDto, Train>();
                        cfg.CreateMap<SeatingClassDto, SeatingClass>();
                    });
        }

        public void ImportClasses(string json)
        {
            IEnumerable<SeatingClass> classes = JsonConvert.DeserializeObject<IEnumerable<SeatingClassDto>>(json)
                .AsQueryable()
                .ProjectTo<SeatingClass>();

            List<SeatingClass> validClasses = new List<SeatingClass>();
            foreach (SeatingClass seatingClass in classes)
            {
                // Class is valid, non-existing and not about to be added.
                if (this.queryHelper.IsEntityValid(seatingClass) && 
                    !validClasses.Any(cl => cl.Name == seatingClass.Name || cl.Abbreviation == seatingClass.Abbreviation) &&
                    !this.queryHelper.IsEntityExisting<SeatingClass>(sc => sc.Name == seatingClass.Name || sc.Abbreviation == seatingClass.Abbreviation))
                {
                    validClasses.Add(seatingClass);
                    Console.WriteLine(RecordSuccessfullyImported, seatingClass.Name);
                }
                else
                {
                    Console.WriteLine(ErrorMessage);
                }
            }

            this.queryHelper.AddRange(validClasses);
        }

        public void ImportStations(string json)
        {
            IEnumerable<Station> stations = JsonConvert.DeserializeObject<IEnumerable<Station>>(json);

            List<Station> validStations = new List<Station>();
            foreach (Station station in stations)
            {
                if (string.IsNullOrEmpty(station.Town))
                {
                    station.Town = station.Name;
                }

                if (!this.queryHelper.IsEntityValid(station) || validStations.Any(s => s.Name == station.Name))
                {
                    Console.WriteLine(ErrorMessage);
                }
                else
                {
                    Console.WriteLine(RecordSuccessfullyImported, station.Name);
                    validStations.Add(station);
                }
            }

            this.queryHelper.AddRange(validStations);
        }

        public void ImportTrains(string json)
        {
            IEnumerable<TrainCreateDto> trainDtos = JsonConvert.DeserializeObject<List<TrainCreateDto>>(json);

            List<Train> validTrains = new List<Train>();
            foreach (TrainCreateDto trainDto in trainDtos)
            {
                Train train = Mapper.Instance.Map<Train>(trainDto);

                // Train is invalid, not existing or to be added.
                if (!this.queryHelper.IsEntityValid(train) ||
                    validTrains.Any(t => t.TrainNumber == train.TrainNumber) ||
                    this.queryHelper.IsEntityExisting<Train>(t => t.TrainNumber == train.TrainNumber))
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                IEnumerable<SeatingClass> seats = Mapper.Instance.Map<List<SeatingClass>>(trainDto.Seats);

                if (seats != null)
                {
                    // Seat class is invalid or not existing.
                    if (seats.Any(s => !this.queryHelper.IsEntityValid(s)) ||
                        seats.Any(s => !this.queryHelper.IsEntityExisting<SeatingClass>(st => st.Name == s.Name &&
                                                                                        st.Abbreviation == s.Abbreviation)))
                    {
                        Console.WriteLine(ErrorMessage);
                        continue;
                    }

                    List<TrainSeat> trainSeats = trainDto.Seats.Select(s => new TrainSeat
                    {
                        SeatingClass = this.queryHelper.SingleOrDefault<SeatingClass>(sc => sc.Name == s.Name),
                        Quantity = s.Quantity ?? -1
                    })
                    .ToList();

                    if (trainSeats.Any(tr => !this.queryHelper.IsEntityValid(tr)))
                    {
                        Console.WriteLine(ErrorMessage);
                        continue;
                    }

                    foreach (TrainSeat trainSeat in trainSeats)
                    {
                        train.TrainSeats.Add(trainSeat);
                    }
                }

                Console.WriteLine(RecordSuccessfullyImported, train.TrainNumber);
                validTrains.Add(train);
            }

            this.queryHelper.AddRange(validTrains);
        }

        public void ImportTrips(string json)
        {
            List<TripDto> trips = JsonConvert.DeserializeObject<List<TripDto>>(
                json,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = "dd/MM/yyyy HH:mm"
                });

            List<Trip> validTrips = new List<Trip>();
            foreach (TripDto tripDto in trips)
            {
                if (tripDto.ArrivalTime == null || tripDto.DepartureTime == null || tripDto.DepartureTime > tripDto.ArrivalTime)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                if (!this.queryHelper.IsEntityExisting<Train>(t => t.TrainNumber == tripDto.Train) ||
                    !this.queryHelper.IsEntityExisting<Station>(st => st.Name == tripDto.OriginStation) ||
                    !this.queryHelper.IsEntityExisting<Station>(st => st.Name == tripDto.DestinationStation))
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                TimeSpan timeDifference;
                if (tripDto.TimeDifference != null && !TimeSpan.TryParseExact(tripDto.TimeDifference, @"hh\:mm", CultureInfo.InvariantCulture, out timeDifference))
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                Trip trip = this.MapFromDatabase(tripDto);

                if (!this.queryHelper.IsEntityValid(trip))
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                validTrips.Add(trip);
                Console.WriteLine($"Trip from {tripDto.OriginStation} to {tripDto.DestinationStation} imported.");
            }

            this.queryHelper.AddRange(validTrips);
        }

        public void ImportCards(string xml)
        {
            XDocument xmlDocument = XDocument.Parse(xml);
            XElement root = xmlDocument.Element("Cards");

            List<CustomerCard> validCards = new List<CustomerCard>();
            foreach (XElement importedCard in root.Elements())
            {
                string name = importedCard.Element("Name").Value;
                int age = int.Parse(importedCard.Element("Age").Value);

                CardType cardType =
                    importedCard.Element("CardType") == null ?
                    CardType.Normal :
                    (CardType)Enum.Parse(typeof(CardType), importedCard.Element("CardType").Value);

                CustomerCard card = new CustomerCard
                {
                    Name = name,
                    Age = age,
                    Type = cardType
                };

                if (!this.queryHelper.IsEntityValid(card))
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                validCards.Add(card);
                Console.WriteLine($"Record {card.Name} successfully imported.");
            }

            this.queryHelper.AddRange(validCards);
        }

        public void ImportTickets(string xml)
        {
            XDocument document = XDocument.Parse(xml);

            XElement ticketsElement = document.Element("Tickets");
            List<Ticket> validTickets = new List<Ticket>();

            foreach (XElement ticketElement in ticketsElement.Elements())
            {
                Ticket ticket = new Ticket();

                decimal price = decimal.Parse(ticketElement.Attribute("price").Value);

                if (price <= 0m)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                ticket.Price = price;

                string seat = ticketElement.Attribute("seat")?.Value;

                if (!this.IsSeatNumberValid(seat) ||
                    !this.queryHelper.IsEntityExisting<SeatingClass>(sc => sc.Abbreviation == seat.Substring(0, 2)))
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                XElement tripElement = ticketElement.Element("Trip");

                string originStationName = tripElement?.Element("OriginStation")?.Value;
                string destinationStationName = tripElement?.Element("DestinationStation")?.Value;

                DateTime departureTime = DateTime.ParseExact(
                    tripElement.Element("DepartureTime").Value,
                    "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture);

                Trip trip = this.queryHelper.SingleOrDefault<Trip>(t =>
                                                            t.OriginStation.Name == originStationName &&
                                                            t.DestinationStation.Name == destinationStationName &&
                                                            t.DepartureTime == departureTime);

                if (trip == null)
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                XElement cardElement = ticketElement.Element("Card");
                if (cardElement != null)
                {
                    string cardName = cardElement.Attribute("Name").Value;
                    CustomerCard card =
                        this.queryHelper.SingleOrDefault<CustomerCard>(
                            c => c.Name == cardName);

                    if (card == null)
                    {
                        Console.WriteLine(ErrorMessage);
                        continue;
                    }

                    ticket.CustomerCard = card;
                }

                if (!this.IsSeatPlaceValid(trip.Train, seat))
                {
                    Console.WriteLine(ErrorMessage);
                    continue;
                }

                ticket.SeatingPlace = seat;
                ticket.Trip = trip;
                validTickets.Add(ticket);
                Console.WriteLine($"Ticket from {originStationName} to {destinationStationName} departing at {departureTime:dd/MM/yyyy HH:mm} imported.");
            }

            this.queryHelper.AddRange(validTickets);
        }

        private bool IsSeatNumberValid(string seat)
        {
            int parsedNumber;
            bool isNumber = int.TryParse(seat.Substring(2), out parsedNumber);

            if (!isNumber)
            {
                return false;
            }

            if (parsedNumber <= 0)
            {
                return false;
            }

            return true;
        }

        private bool IsSeatPlaceValid(Train train, string seat)
        {
            string abbr = seat.Substring(0, 2);
            int seatNumber = int.Parse(seat.Substring(2));

            return train.TrainSeats.Any(ts => ts.SeatingClass.Abbreviation == abbr && ts.Quantity >= seatNumber);
        }

        private Trip MapFromDatabase(TripDto tripDto)
        {
            Trip trip = new Trip();
            trip.ArrivalTime = tripDto.ArrivalTime.Value;
            trip.DepartureTime = tripDto.DepartureTime.Value;

            trip.Train = this.queryHelper.SingleOrDefault<Train>(t => t.TrainNumber == tripDto.Train);
            trip.OriginStation = this.queryHelper.SingleOrDefault<Station>(st => st.Name == tripDto.OriginStation);
            trip.DestinationStation = this.queryHelper.SingleOrDefault<Station>(st => st.Name == tripDto.DestinationStation);
            trip.Status = tripDto.Status.HasValue ? tripDto.Status.Value : TripStatus.OnTime;

            if (!string.IsNullOrEmpty(tripDto.TimeDifference))
            {
                trip.TimeDifference = TimeSpan.ParseExact(tripDto.TimeDifference, @"hh\:mm", CultureInfo.InvariantCulture);
            }

            return trip;
        }
    }
}
