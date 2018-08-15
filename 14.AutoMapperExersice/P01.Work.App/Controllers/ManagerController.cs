using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using P01.Work.App.Contracts;
using P01.Work.App.Core.Dtos;
using P01.Work.Data;

namespace P01.Work.App.Controllers
{
    public class ManagerController:IManagerController
    {
        private readonly WorkDbContext context;

        public ManagerController(WorkDbContext context)
        {
            this.context = context;
        }
        public void SetManager(int empId, int managerId)
        {
            var employee = context.Employees.Find(empId);
            var manager = context.Employees.Find(managerId);

            if (employee == null || manager == null)
            {
                throw new ArgumentException("Invalid Id");
            }
            employee.Manager = manager;
            this.context.SaveChanges();
        }

        public ManagerDto GetManagerInfo(int emplId)
        {
            var employee = context.Employees
                .Where(x => x.Id == emplId)
                .ProjectTo<ManagerDto>()
                .SingleOrDefault();

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }
            return employee;
        }
    }
}
