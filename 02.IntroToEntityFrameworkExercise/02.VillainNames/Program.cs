using _01.InitialSetup;
using System;
using System.Data.SqlClient;

namespace _02.VillainNames
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string minionsInfo = @"select v.Name,count(m.MinionId) as MinionsCount from Villains as v join MinionsVillains m on m.VillainId = v.Id group by v.Name having COUNT(m.MinionId)>= 3 order by MinionsCount DESC";

                using (var command = new SqlCommand(minionsInfo,connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nameVill = (string) reader["Name"];
                            int count = (int)reader["MinionsCount"];
                            Console.WriteLine($"{nameVill} - {count}");
                        }
                        
                    }
                }





                connection.Close();
            }
        }
    }
}
