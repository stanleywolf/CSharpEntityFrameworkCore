using System;
using System.Collections.Generic;
using System.Text;
using P01.Work.App.Contracts;

namespace P01.Work.App.Commands
{
    public class SetManagerCommand:ICommand
    {
        private readonly IManagerController controller;

        public SetManagerCommand(IManagerController controller)
        {
            this.controller = controller;
        }

        public string Execute(string[] args)
        {
            int employeeid = int.Parse(args[0]);
            int managerId = int.Parse(args[1]);

            this.controller.SetManager(employeeid,managerId);

            return "Manager set successfully";
        }
    }
}
