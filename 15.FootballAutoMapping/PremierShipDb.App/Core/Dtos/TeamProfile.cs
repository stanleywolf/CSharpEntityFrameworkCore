using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PremierShipDb.Models;

namespace PremierShipDb.App.Core.Dtos
{
   public class TeamProfile:Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamDto>().ReverseMap();
            CreateMap<Team, TeamPersonalInfoDto>().ReverseMap();

        }
    }
}
