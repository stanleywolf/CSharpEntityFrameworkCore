﻿using System;
using System.Collections.Generic;
using System.Text;

namespace P01.Work.App.Core.Dtos
{
    public class EmployeePersonalInfoDto
    {
        
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public decimal Salary { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }
    }
}
