using _01.InitialSetup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _05.ChangeTownNameCasing
{
    class Program
    {
        static void Main(string[] args)
        {
            var countryName = Console.ReadLine();

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                bool hasCountry = ValidateCountry(countryName, connection);
                bool hasTowns = ValidateHasTowns(countryName, connection);

                if (!hasTowns || !hasCountry)
                {
                    Console.WriteLine($"No town names were affected.");
                }

                List<string> townsToAffect = GetTowns(countryName, connection);

                ChangeTownsToUpper(townsToAffect, connection);

                PrintTownToAffect(townsToAffect);

                connection.Close();
            }
        }

        private static void ChangeTownsToUpper(List<string> townsToAffect, SqlConnection connection)
        {
            foreach (var town in townsToAffect)
            {
                string changeTownChar = @"update Towns set Name = upper(Name) where Name = @townName";

                using (SqlCommand command = new SqlCommand(changeTownChar, connection))
                {
                    command.Parameters.AddWithValue("@townName", town);
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void PrintTownToAffect(List<string> townsToAffect)
        {
            Console.WriteLine($"{townsToAffect.Count} town names were affected.");
            Console.Write("[");
            Console.Write(string.Join(", ", townsToAffect));
            Console.WriteLine("]");
        }

        private static List<string> GetTowns(string countryName, SqlConnection connection)
        {
            List<string> towns = new List<string>();
            string townsSql = @"select t.Name from Towns as t join Countries as c on c.Id = t.CountryCode where c.Name = @countryName";

            using (SqlCommand command = new SqlCommand(townsSql,connection))
            {
                command.Parameters.AddWithValue("@countryName", countryName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        towns.Add((string)reader[0]); 
                    }
                }
            }
            return towns;
        }

        private static bool ValidateCountry(string countryName, SqlConnection connection)
        {
            var countryIdSql = @"select Id from Countries where Name = @counryName";

            using (SqlCommand command = new SqlCommand(countryIdSql,connection))
            {
                command.Parameters.AddWithValue("@counryName", countryName);
                
                if (command.ExecuteScalar() == null)
                {
                    return false;
                }
                return true;
            }
        }

        private static bool ValidateHasTowns(string countryName, SqlConnection connection)
        {
            var countryInfo = @" select count(*) from Towns as t join Countries as c on c.Id = t.CountryCode where c.Name = @counryName";

            using (SqlCommand command = new SqlCommand(countryInfo,connection))
            {
                command.Parameters.AddWithValue("@counryName", countryName);
                
                if ((int)command.ExecuteScalar() == 0)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
