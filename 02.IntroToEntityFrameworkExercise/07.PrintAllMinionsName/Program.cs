using _01.InitialSetup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _07.PrintAllMinionsName
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                List<string> minions = GetMinionsName(connection);
                int counter = minions.Count;
                for (int i = 0; i < counter; i++)
                {
                    if (minions.Count <= 0)
                    {
                        return;
                    }
                    Console.WriteLine(minions[0]);
                    minions.RemoveAt(0);
                    if (minions.Count <= 0)
                    {
                        return;
                    }
                   Console.WriteLine(minions[minions.Count -1]);
                    minions.RemoveAt(minions.Count - 1);
                }


                connection.Close();
            }

            
        }

        private static List<string> GetMinionsName(SqlConnection connection)
        {
            List<string> minions = new List<string>();

            var minionSql = @"select Name from Minions";

            using (SqlCommand command = new SqlCommand(minionSql,connection))
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var minion = (string)reader[0];
                    minions.Add(minion);
                }
            }
            return minions;

        }
    }
}
