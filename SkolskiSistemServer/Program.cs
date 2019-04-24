using System;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.ServiceModel;
using SkolskiSistemCommon;

namespace SkolskiSistemServer
{
    internal class Program
    {
        private const string Address = "net.tcp://localhost:8000/";

        public static string SqlString => "Data Source = 192.168.0.17;\n" +
                                          "Initial Catalog = SkolskiSistem;\n" +
                                          "User ID={your_username};\n".Replace("{your_username}", Environment.GetEnvironmentVariable("dbUsername")) +
                                          "Password={your_password};".Replace("{your_password}", Environment.GetEnvironmentVariable("dbPassword"));

        public static void Main(string[] args)
        {
            // Create the tables if they don't already exist
            using (var connection = new SqlConnection(SqlString))
            {
                connection.Open();
                if (connection.State != ConnectionState.Open)
                {
                    throw new Exception("Connection to the database öppnade inte!");
                }
                Console.WriteLine("Connection " + connection.State);
                new SqlCommand(Queries.CreateSkola, connection).ExecuteNonQuery();
                new SqlCommand(Queries.CreateSmer, connection).ExecuteNonQuery();
                new SqlCommand(Queries.CreateUcenik, connection).ExecuteNonQuery();
                Console.WriteLine("Initialized tables.");
            }

            var serviceHost = new ServiceHost(typeof(Service));
            serviceHost.AddServiceEndpoint(typeof(IService), new NetTcpBinding(), Address);

            serviceHost.Open();
            Console.WriteLine("Server up on port 8000.");
            while (Console.ReadKey(true).KeyChar != 'x') { }
            serviceHost.Close();
        }
    }
}