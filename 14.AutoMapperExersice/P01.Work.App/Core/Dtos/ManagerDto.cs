using System;
using System.Collections.Generic;
using System.Text;

namespace P01.Work.App.Core.Dtos
{
    public class ManagerDto
    {
        public ManagerDto()
        {
            this.EmployeeDto = new List<EmployeeDto>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int EmployeeCount => EmployeeDto.Count;

        public ICollection<EmployeeDto> EmployeeDto { get; set; }
    }
}
