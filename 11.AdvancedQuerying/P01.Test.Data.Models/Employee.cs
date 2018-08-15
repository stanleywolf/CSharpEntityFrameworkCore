using System;
using System.Collections;
using System.Collections.Generic;

namespace P01.Test.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public int? Age { get; set; }

        public virtual ICollection<Project> ManagerOfProject { get; set; }
    }
}
