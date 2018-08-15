using System;
using System.Collections.Generic;
using System.Text;
using P01.Work.App.Contracts;

namespace P01.Work.App.Commands
{
    public class EmployeeInfoCommand:ICommand
    {
        private readonly IEmployeeConrtoller conrtoller;

        public EmployeeInfoCommand(IEmployeeConrtoller conrtoller)
        {
            this.conrtoller = conrtoller;
        }
        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);
            var employeeDto = this.conrtoller.GetEmployeeInfo(id);

            return $"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} -  ${employeeDto.Salary:f2}";
        }
    }
}
