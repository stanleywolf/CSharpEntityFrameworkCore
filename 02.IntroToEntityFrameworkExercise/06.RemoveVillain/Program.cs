using _01.InitialSetup;
using System;
using System.Data.SqlClient;

namespace _06.RemoveVillain
{
    class Program
    {
        static void Main(string[] args)
        {
            var villainId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                bool hasVillain = HasVillain(villainId, connection,transaction);

                if (!hasVillain)
                {
                    Console.WriteLine($"No such villain was found.");
                }
                else
                {
                    try
                    {
                        int affectedRows = ReleaseMinions(villainId, connection, transaction);
                        string villainName = GetVillainName(villainId, connection, transaction);
                        DeleteVillain(villainId, connection, transaction);

                        Console.WriteLine($"{villainName} was deleted!");
                        Console.WriteLine($"{affectedRows} minions were released.");
                    }
                    catch (SqlException e)
                    {
                        transaction.Rollback();
                    }
                }
                connection.Close();
            }
        }

        private static void DeleteVillain(int villainId, SqlConnection connection,SqlTransaction transaction)
        {
            string villIdSql = @"delete from Villains where Id = @Id";
            using (SqlCommand command = new SqlCommand(villIdSql,connection,transaction))
            {
                command.Parameters.AddWithValue("@Id", villainId);
                command.ExecuteNonQuery();
            }
        }

        private static string GetVillainName(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            string nameSql = @"select Name from Villains where Id = @Id";

            using (SqlCommand command = new SqlCommand(nameSql,connection,transaction))
            {
                command.Parameters.AddWithValue("@Id", villainId);
                return (string) command.ExecuteScalar();
            }
        }

        private static int ReleaseMinions(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            string villIdSql = @"delete from MinionsVillains where VillainId = @villainId";

            using (SqlCommand command = new SqlCommand(villIdSql,connection,transaction))
            {
                command.Parameters.AddWithValue("@villainId", villainId);

                //return nums of affected rows??
                return command.ExecuteNonQuery();
            }
        }

        private static bool HasVillain(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            var villIdSql = "select Id from Villains where Id = @villainId";

            using (SqlCommand command = new SqlCommand(villIdSql,connection,transaction))
            {
                command.Parameters.AddWithValue("@villainId", villainId);
                if (command.ExecuteScalar() == null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
