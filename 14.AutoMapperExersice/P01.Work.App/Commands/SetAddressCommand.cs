using System;
using System.Collections.Generic;
using System.Text;
using P01.Work.App.Contracts;

namespace P01.Work.App.Commands
{
    public class SetAddressCommand:ICommand
    {
        private readonly IEmployeeConrtoller conrtoller;
        public SetAddressCommand(IEmployeeConrtoller conrtoller)
        {
            this.conrtoller = conrtoller;
        }
        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);
            string addresss = args[1];

            this.conrtoller.SetAddress(id, addresss);

            return "Command execute successfully!";
        }
    }
}
