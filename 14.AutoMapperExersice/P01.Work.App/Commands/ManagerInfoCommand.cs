using System;
using System.Collections.Generic;
using System.Text;
using P01.Work.App.Contracts;

namespace P01.Work.App.Commands
{
   public  class ManagerInfoCommand:ICommand
   {
       private readonly IManagerController controller;

       public ManagerInfoCommand(IManagerController controller)
       {
           this.controller = controller;
       }

        public string Execute(string[] args)
        {
            int employeeId = int.Parse(args[0]);

            var managerDto = controller.GetManagerInfo(employeeId);
            var sb = new StringBuilder();

            sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.EmployeeCount}");

            foreach (var employee in managerDto.EmployeeDto)
            {
                sb.AppendLine($"    - {employee.FirstName} {employee.LastName} - ${employee.Salary:f2}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
