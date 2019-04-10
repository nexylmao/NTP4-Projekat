using System;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Description;
using skolski_sistem_common;

namespace skolski_sistem_server
{
    internal class Program
    {
        public static string Path => "net.tcp://localhost:5694/api-ss/";

        public static void Main(string[] args)
        {
            try
            {
                var serviceHost = new ServiceHost(typeof(Service));
                serviceHost.AddServiceEndpoint(typeof(IService), new NetTcpBinding(), new Uri(Path));
                serviceHost.Open();
                Console.WriteLine("Server up on port 5694");
                Console.ReadKey(true);
                Console.WriteLine("Server stopped.");
                serviceHost.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}