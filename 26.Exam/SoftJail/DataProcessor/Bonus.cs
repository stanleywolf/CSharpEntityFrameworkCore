﻿using System.Linq;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;

    public class Bonus
    {
        public static string ReleasePrisoner(SoftJailDbContext context, int prisonerId)
        {
            var prisoner = context.Prisoners.SingleOrDefault(x => x.Id == prisonerId);

            if (prisoner.ReleaseDate == null)
            {
                return $"Prisoner {prisoner.FullName} is sentenced to life";
            }
            else
            {
                prisoner.ReleaseDate = DateTime.Now;
                prisoner.CellId = null;
                
                context.SaveChanges();

                return $"Prisoner {prisoner.FullName} released";
            }
        }
    }
}
