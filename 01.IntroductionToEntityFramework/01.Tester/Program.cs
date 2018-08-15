using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _01.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString =
                @"Server=STANCHO-PC\SQLEXPRESS01;" +
                @"Database=SoftUni;" +
                @"Integrated Security=true;";

            var connection = new SqlConnection(connectionString);

            connection.Open();

            //connection.Close();       to close conn/but use
            //connection.Dispose();          using
          // using (connection)
          // {
          //     var commandText = "SELECT COUNT(*) FROM //Employees";
          //     var command = new SqlCommand//(commandText,connection);
          //
          //     var employeesCount = command.ExecuteScalar();
          //     Console.WriteLine(employeesCount);
          // }
           // using (connection)
           // {
           //     var town = Console.ReadLine();
           //     var commandTextTwo = $"DELETE FROM Towns WHERE //Name = 'Samokov'";
           //
           //     var commandTwo = new SqlCommand//(commandTextTwo,connection);
           //
           //     var affectetRoes = commandTwo.ExecuteNonQuery();
           //     Console.WriteLine($"Rows affected: //{affectetRoes}");
           // }

            using (connection)
            {
                var commandTextThree = "SELECT FirstName,LastName,JobTitle FROM Employees";
                var commandThree = new SqlCommand(commandTextThree,connection);

                var reader = commandThree.ExecuteReader();

                var employees = new List<Employees>();
                while (reader.Read())
                {
                    var firstName = (string)reader["FirstName"];
                    var lastName = (string)reader["LastName"];
                    var jobTitle = (string)reader["JobTitle"];

                    Employees employee = new Employees(firstName,lastName,jobTitle);

                    employees.Add(employee);
                }

                var groups = employees.GroupBy(e => e.JobTitle).OrderBy(a => a.Key);

                foreach (var group in groups)
                {
                    int counter = 1;
                    Console.WriteLine(group.Key);
                    foreach (var g in group)
                    {
                        Console.WriteLine($"    {counter}. {g.FirstName} {g.LastName}");
                        counter++;
                    }
                }
            }
        }
    }
}
