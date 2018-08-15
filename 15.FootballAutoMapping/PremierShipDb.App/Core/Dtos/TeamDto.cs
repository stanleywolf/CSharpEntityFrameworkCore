using System;
using System.Collections.Generic;
using System.Text;

namespace PremierShipDb.App.Core.Dtos
{
    public class TeamDto
    {
        public int TeamId { get; set; }
        
        public string Name { get; set; }
        
        public string Stadium { get; set; }
        
        public decimal Budget { get; set; }
        
        public double WorldRanking { get; set; }
    }
}
