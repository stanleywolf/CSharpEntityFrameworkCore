using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using P01.Work.App.Contracts;
using P01.Work.App.Core.Dtos;
using P01.Work.Data;
using P01.Work.Models;

namespace P01.Work.App.Controllers
{
    public class EmployeeController:IEmployeeConrtoller
    {
        private readonly WorkDbContext context;
        private readonly IMapper mapper;

        public EmployeeController(WorkDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void AddEmployee(EmployeeDto employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);

            this.context.Employees.Add(employee);
            this.context.SaveChanges();

        }

        public void SetBirthday(int employeeId, DateTime date)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid id");
            }
            employee.Birthday = date;

            this.context.SaveChanges();
        }

        public void SetAddress(int employeeId, string address)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid id");
            }
            employee.Address = address;

            this.context.SaveChanges();
        }

        public EmployeeDto GetEmployeeInfo(int employeeId)
        {
            var employee = context.Employees
                .Find(employeeId);
            var employeeDto = mapper.Map<EmployeeDto>(employee);

            if (employee == null)
            {
                throw  new ArgumentException("Invalid id");
            }
            return employeeDto;
        }

        public EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId)
        {
            var employee = context.Employees
                .Find(employeeId);
            var employeeDto = mapper.Map<EmployeePersonalInfoDto>(employee);

            if (employee == null)
            {
                throw new ArgumentException("Invalid id");
            }
            return employeeDto;
        }
    }
}
