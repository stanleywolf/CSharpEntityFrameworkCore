using System;
using System.Collections.Generic;
using System.Text;
using PremierShipDb.App.Contracts;

namespace PremierShipDb.App.Commands
{
    public class TeamInfoCommand:ICommand
    {
        private readonly ITeamController controller;

        public TeamInfoCommand(ITeamController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            var teamId = int.Parse(args[0]);

            var teamDto = this.controller.TeamInfo(teamId);

            return
                $"TeamId: {teamDto.TeamId} {teamDto.Name} - have world ranking {teamDto.WorldRanking}/100\nand budget £{teamDto.Budget:f2}";
        }
    }
}
