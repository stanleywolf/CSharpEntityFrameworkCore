namespace Stations.Dto.Import
{
    using System.Collections.Generic;

    using Stations.Models;

    public class TrainCreateDto
    {
        public string TrainNumber { get; set; }

        public TrainType? Type { get; set; }

        public ICollection<SeatsClassPerTrainDto> Seats { get; set; }
    }
}
