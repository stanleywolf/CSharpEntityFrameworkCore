using System;
using System.Collections.Generic;
using System.Text;


public class Employees : Person
{
    public string JobTitle { get; set; }

    public Employees(string firstName, string lastName, string jobTitle) : base(firstName, lastName)
    {
        this.JobTitle = jobTitle;
    }

    public override string ToString()
    {
        return base.ToString() + " " + $"{this.JobTitle}";
    }
}