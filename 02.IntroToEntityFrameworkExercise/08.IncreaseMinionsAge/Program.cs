using _01.InitialSetup;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace _08.IncreaseMinionsAge
{
    class Program
    {
        static void Main(string[] args)
        {
            var minionsId = Console.ReadLine().Split().Select(int.Parse).ToArray();
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                for (int i = 0; i < minionsId.Length; i++)
                {
                    var minionId = minionsId[i];                    
                    IncreaseMinionsAge(minionId, connection);
                    MakeNameToTitleCase(minionId, connection);
                }
                PrintAllMinions(connection);

                connection.Close();
            }           
        }

        private static void PrintAllMinions(SqlConnection connection)
        {
            var minions = @"select Name,Age from Minions";
            using (SqlCommand command =new SqlCommand(minions,connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
                }
            }
            
            
        }

        private static void MakeNameToTitleCase(int minionId, SqlConnection connection)
        {
            var minionChangeName =
                @"update Minions SET Name = UPPER(LEFT(Name, 1)) + LOWER(RIGHT(Name, LEN(Name)- 1))where Id = @Id";

            using (SqlCommand command = new SqlCommand(minionChangeName, connection))
            {
                command.Parameters.AddWithValue("@Id", minionId);
                command.ExecuteNonQuery();
            }
        }

        private static void IncreaseMinionsAge(int minionId, SqlConnection connection)
        {
            var changeAge = @"update Minions set Age+=1 where Id = @Id";

            using (SqlCommand command = new SqlCommand(changeAge,connection))
            {
                command.Parameters.AddWithValue("@Id", minionId);
                command.ExecuteNonQuery();
            }
        }
       
    }
}
