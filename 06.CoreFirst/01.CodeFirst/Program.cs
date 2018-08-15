using System;
using System.Collections.Generic;
using System.Linq;
using _01.CodeFirst.Models;

namespace _01.CodeFirst
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new AwesomHotelDbContext();
            //var room = new Room()
            //{
            //    BedCount = 1,
            //    Cost = 27.55m,
            //    IsAvailable = true,
            //    Number = 555,
            //    RoomNickName = "Legloto",
            //    RoomType = RoomType.Standard,
            //    RoomsKeyCards = new List<RoomsKeyCards>()
            //    {
            //        new RoomsKeyCards()
            //        {
            //            KeyCard = new KeyCard()
            //            {
            //                CardNumber = 1500
            //            }
            //        }
            //    }
            //};
            //context.Rooms.Add(room);
            //context.SaveChanges();
            var room = context.Rooms.Where(x => x.Id == 2).Select(x => new
            {
                RoomsKeyCards = x.RoomsKeyCards,
                Bed = x.BedCount
            }).ToArray();

            foreach (var r in room)
            {

                Console.WriteLine(r.Bed);
            }
        }
    }
}
