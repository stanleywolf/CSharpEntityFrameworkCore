using System;
using System.Collections.Generic;
using System.Text;
using PremierShipDb.App.Contracts;

namespace PremierShipDb.App.Commands
{
    public class SetDateFoundCommand:ICommand
    {
        private readonly ITeamController controller;

        public SetDateFoundCommand(ITeamController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            var teamId = int.Parse(args[0]);

            DateTime date = DateTime.ParseExact(args[1],"dd-MM-yyyy",null);

            this.controller.SetDateFound(teamId,date);

            return $"DateFounded added successfully";
        }
    }
}
