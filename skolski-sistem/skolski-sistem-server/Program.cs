using System;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Description;
using skolski_sistem_common;

namespace skolski_sistem_server
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var path = "net.tcp://localhost:5694/api-ss/";
                var serviceHost = new ServiceHost(typeof(Service));
                serviceHost.AddServiceEndpoint(typeof(IService), new NetTcpBinding(), new Uri(path));
                serviceHost.Open();
                Console.WriteLine("Server up on port 5694");

                char c;
                do
                {
                    c = Console.ReadKey(true).KeyChar;
                } while (c != 'x');

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