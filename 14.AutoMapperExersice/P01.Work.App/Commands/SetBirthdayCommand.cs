using System;
using System.Collections.Generic;
using System.Text;
using P01.Work.App.Contracts;

namespace P01.Work.App.Commands
{
    public class SetBirthdayCommand:ICommand
    {
        private readonly IEmployeeConrtoller conrtoller;
        public SetBirthdayCommand(IEmployeeConrtoller conrtoller)
        {
            this.conrtoller = conrtoller;
        }
        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);
            DateTime date = DateTime.ParseExact(args[1],"dd-MM-yyyy",null);

            this.conrtoller.SetBirthday(id,date);

            return "Command execute successfully";
        }
    }
}
