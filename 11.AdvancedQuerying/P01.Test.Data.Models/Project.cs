using System;
using System.Collections.Generic;
using System.Text;

namespace P01.Test.Data.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
