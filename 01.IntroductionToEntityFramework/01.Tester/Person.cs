using System;
using System.Collections.Generic;
using System.Text;


public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Person(string firstName,string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public override string ToString()
    {
        return $"{this.FirstName} {this.LastName}";
    }
}

