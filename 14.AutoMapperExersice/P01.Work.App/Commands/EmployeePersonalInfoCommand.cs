using System;
using System.Collections.Generic;
using System.Text;
using P01.Work.App.Contracts;

namespace P01.Work.App.Commands
{
    public class EmployeePersonalInfoCommand:ICommand
    {
        private readonly IEmployeeConrtoller controller;

        public EmployeePersonalInfoCommand(IEmployeeConrtoller conrtoller )
        {
            this.controller = conrtoller;
        }

        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);

            var empDto = this.controller.GetEmployeePersonalInfo(id);

            return $"ID: {empDto.Id} - {empDto.FirstName} {empDto.LastName} - ${empDto.Salary:f2}\n" +
                   $"Birthday: {empDto.Birthday.ToString("dd-MM-yyyy")}\n" +
                   $"Address: {empDto.Address}";
        }
    }
}
