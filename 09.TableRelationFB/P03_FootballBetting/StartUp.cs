using System;
using P03_FootballBetting.Data;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            FootballBettingContext context = new FootballBettingContext();
            using (context)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            
        }
    }
}
