using System;
using System.Data.SqlClient;
using skolski_sistem_common;

namespace skolski_sistem_server
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var sqlConnection = new SqlConnection(adjustConnectionString()))
            {
                sqlConnection.Open();
                
                /*
                 * Creating the Tables (if that is necessary)
                 */
                var skolaCreate = new SqlCommand(Skola.SqlQueries.Create, sqlConnection);
                skolaCreate.ExecuteNonQuery();
                
                var smerCreate = new SqlCommand(Smer.SqlQueries.Create, sqlConnection);
                smerCreate.ExecuteNonQuery();
                
                var ucenikCreate = new SqlCommand(Ucenik.SqlQueries.Create, sqlConnection);
                ucenikCreate.ExecuteNonQuery();
            }

            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}