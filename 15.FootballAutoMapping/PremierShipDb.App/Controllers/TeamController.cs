using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PremierShipDb.App.Contracts;
using PremierShipDb.App.Core.Dtos;
using PremierShipDb.Data;
using PremierShipDb.Models;

namespace PremierShipDb.App.Controllers
{
    public class TeamController:ITeamController
    {
        private readonly PremierShipDbContext context;
        private readonly IMapper mapper;

        public TeamController(PremierShipDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public void AddTeam(TeamDto teamDto)
        {
            var team = mapper.Map<Team>(teamDto);

            this.context.Add(team);
            this.context.SaveChanges();
        }

        public void SetDateFound(int teamId, DateTime date)
        {
            var team = this.context.Teams.Find(teamId);

            if (team == null)
            {
                throw new ArgumentException("Invalid Id!");
            }
            team.DateFounded = date;
            this.context.SaveChanges();
        }

        public void SetAddress(int teamId, string address)
        {
            var team = this.context.Teams.Find(teamId);

            if (team == null)
            {
                throw new ArgumentException("Invalid Id!!");
            }
            team.AddressTown = address;
            this.context.SaveChanges();
        }

        public TeamDto TeamInfo(int teamId)
        {
            var team = context.Teams
                .Find(teamId);
            var teamDto = mapper.Map<TeamDto>(team);

            if (team == null)
            {
                throw new ArgumentException("Invalid Id!");
            }
            return teamDto;
        }

        public TeamPersonalInfoDto TeamPersonalInfo(int teamId)
        {
            throw new NotImplementedException();
        }
    }
}
