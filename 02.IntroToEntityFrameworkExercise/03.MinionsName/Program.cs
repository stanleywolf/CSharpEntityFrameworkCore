using System;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace _03.MinionsName
{
    class Program
    {
        static void Main(string[] args)
        {
            int villainsId = int.Parse(Console.ReadLine());
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string villainName = GetVillainName(villainsId, connection);

                if (villainName == null)
                {
                    Console.WriteLine($"No villain with ID {villainsId} exists in the database.");
                }
                else
                {
                    Console.WriteLine($"Villain: {villainName}");
                    PrintNames(villainsId, connection);
                }
                
                
                

                connection.Close();
            }
        }

        private static void PrintNames(int villainsId, SqlConnection connection)
        {
            string minionSql =@"select Name,Age from Minions as m join MinionsVillains as mv on mv.MinionId = m.Id where mv.VillainId = @id";
            using (SqlCommand command = new SqlCommand(minionSql,connection))
            {
                command.Parameters.AddWithValue("@id", villainsId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("(no minions)");
                    }
                    else
                    {
                        int counter = 1;
                        while (reader.Read())
                        {
                            Console.WriteLine($"{counter}.{reader[0]} {reader[1]}");
                            counter++;
                        }
                    }
                }
            }
        }

        private static string GetVillainName(int villainsId, SqlConnection connection)
        {
            string nameSql = "SELECT Name FROM Villains WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(nameSql,connection))
            {
                command.Parameters.AddWithValue("@id", villainsId);
                return (string) command.ExecuteScalar();
            }
        }
    }
}
