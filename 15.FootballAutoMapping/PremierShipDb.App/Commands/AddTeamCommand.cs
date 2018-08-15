using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PremierShipDb.App.Contracts;
using PremierShipDb.App.Core.Dtos;
using PremierShipDb.Models;

namespace PremierShipDb.App.Commands
{
    public class AddTeamCommand:ICommand
    {
        private readonly ITeamController controller;

        public AddTeamCommand(ITeamController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            string name = args[0];
            string stadium = args[1];
            decimal budget = decimal.Parse(args[2]);
            double ranking = double.Parse(args[3]);

            var teamDto = new TeamDto()
            {
                Name = name,
                Stadium = stadium,
                Budget = budget,
                WorldRanking = ranking
            };
            this.controller.AddTeam(teamDto);

            return $"{name} added successfully!";
        }
    }
}
