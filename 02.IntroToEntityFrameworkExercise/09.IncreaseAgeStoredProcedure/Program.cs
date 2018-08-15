using _01.InitialSetup;
using System;
using System.Data.SqlClient;

namespace _09.IncreaseAgeStoredProcedure
{
    class Program
    {
        static void Main(string[] args)
        {
            int minionId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                IncreaseAgeWithProcedure(minionId, connection);

                PrintAllMinions(connection);
                connection.Close();
            }
        }

        private static void IncreaseAgeWithProcedure(int minionId, SqlConnection connection)
        {
            string execProc = @"EXEC usp_GetOlder @Id";
            using (SqlCommand command = new SqlCommand(execProc,connection))
            {
                command.Parameters.AddWithValue("@Id", minionId);
                command.ExecuteNonQuery();
            }
        }

        private static void PrintAllMinions(SqlConnection connection)
        {
            var minions = @"select Name,Age from Minions";
            using (SqlCommand command = new SqlCommand(minions, connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
                }
            }


        }
    }
}
