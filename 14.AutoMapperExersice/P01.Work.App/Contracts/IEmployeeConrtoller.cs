using System;
using System.Collections.Generic;
using System.Text;
using P01.Work.App.Core.Dtos;

namespace P01.Work.App.Contracts
{
    public interface IEmployeeConrtoller
    {
        void AddEmployee(EmployeeDto employeeDto);

        void SetBirthday(int employeeId, DateTime date);

        void SetAddress(int employeeId, string address);

        EmployeeDto GetEmployeeInfo(int employeeId);

        EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId);
    }
}
