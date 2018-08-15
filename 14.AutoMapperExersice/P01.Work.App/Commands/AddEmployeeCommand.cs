using System;
using System.Collections.Generic;
using System.Text;
using P01.Work.App.Contracts;
using P01.Work.App.Core.Dtos;

namespace P01.Work.App.Commands
{
    public class AddEmployeeCommand:ICommand
    {

        private readonly IEmployeeConrtoller conrtoller;
        public AddEmployeeCommand(IEmployeeConrtoller conrtoller)
        {
            this.conrtoller = conrtoller;
        }


        public string Execute(string[] args)
        {
            string firstName = args[0];
            string lastName = args[1];
            decimal salary = decimal.Parse(args[2]);

            EmployeeDto employeeDto = new EmployeeDto()
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };

            this.conrtoller.AddEmployee(employeeDto);

            return $"Employee {firstName} {lastName} was added successfully";
        }
    }
}
