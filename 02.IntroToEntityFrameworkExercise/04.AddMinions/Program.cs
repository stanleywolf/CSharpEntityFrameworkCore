using System;
using System.Data.SqlClient;
using _01.InitialSetup;
namespace _04.AddMinions
{
    class Program
    {
        static void Main(string[] args)
        {
            var minionInfo = Console.ReadLine().Split();
            var villainInfo = Console.ReadLine().Split();

            var minionName = minionInfo[1];
            var age = int.Parse(minionInfo[2]);
            var townName = minionInfo[3];

            var villainName = villainInfo[1];

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                int townId = GetTownId(townName, connection);
                int villianId = GetVillainId(villainName, connection);
                int minionId = InsertMinionAndGetId(minionName,age,townId,connection);
                AssignMinionToVillain(villianId, minionId, connection);

                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");

                connection.Close();
            }
        }

        private static void AssignMinionToVillain(int villianId, int minionId, SqlConnection connection)
        {
            var insertToMV = "insert into MinionsVillains (MinionId,VillainId) values (@minionId,@villainId)";
            using (SqlCommand command = new SqlCommand(insertToMV,connection))
            {
                command.Parameters.AddWithValue("@minionId", minionId);
                command.Parameters.AddWithValue("@villainId", villianId);

                command.ExecuteNonQuery();                
            }
        }

        private static int InsertMinionAndGetId(string minionName, int age, int townId, SqlConnection connection)
        {
            var minionsInsert = "insert into Minions(Name,Age,TownId) values (@name,@age,@townId)";

            using (SqlCommand command = new SqlCommand(minionsInsert,connection))
            {
                command.Parameters.AddWithValue("@name", minionName);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@townId", townId);

                command.ExecuteNonQuery();
            }
            string minionSql = "select Id from Minions where Name = @name";
            using (SqlCommand command = new SqlCommand(minionSql,connection))
            {
                command.Parameters.AddWithValue("@name", minionName);
                return (int) command.ExecuteScalar();
            }
        }

        private static int GetVillainId(string villainName, SqlConnection connection)
        {
            var villainSql = "select Id from Villains where Name = @Name";

            using (SqlCommand command = new SqlCommand(villainSql, connection))
            {
                command.Parameters.AddWithValue("@Name", villainName);

                if (command.ExecuteScalar() == null)
                {
                    InsertIntoVillains(villainName, connection);
                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }
                return (int)command.ExecuteScalar();
            }
        }

        private static void InsertIntoVillains(string villainName, SqlConnection connection)
        {
            var insertVillian = "insert into Villains (Name) values (@villainName)";

            using (SqlCommand command = new SqlCommand(insertVillian, connection))
            {
                command.Parameters.AddWithValue("@villainName", villainName);
                command.ExecuteNonQuery();
                
            }
        }

        private static int GetTownId(string townName, SqlConnection connection)
        {
            var townSql = "select Id from Towns where Name = @Name";

            using (SqlCommand command = new SqlCommand(townSql,connection))
            {
                command.Parameters.AddWithValue("@Name", townName);

                if (command.ExecuteScalar() == null)
                {
                    InsertIntoTowns(townName, connection);
                    Console.WriteLine($"Town {townName} was added to the database.");
                }
                return (int) command.ExecuteScalar();
            }
        }

        private static void InsertIntoTowns(string townName, SqlConnection connection)
        {
            var insertTown = "insert into Towns (Name) values (@townName)";

            using (SqlCommand command = new SqlCommand(insertTown, connection))
            {
                command.Parameters.AddWithValue("@townName", townName);
                command.ExecuteNonQuery();
                
            }
        }
    }
}
