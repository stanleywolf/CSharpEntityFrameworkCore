using System;
using System.Collections.Generic;
using System.Text;
using PremierShipDb.App.Contracts;

namespace PremierShipDb.App.Commands
{
    public class SetAddressCommand:ICommand
    {
        private readonly ITeamController controller;

        public SetAddressCommand(ITeamController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            int teamId = int.Parse(args[0]);
            string address = args[1];

            this.controller.SetAddress(teamId,address);

            return "Address set successfully";
        }
    }
}
