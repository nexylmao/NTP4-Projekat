using System;
using System.ServiceModel;
using Newtonsoft.Json;
using skolski_sistem_common;

namespace skolski_sistem_client
{
    internal class Program
    {
        public static string Path => "net.tcp://localhost:5694/api-ss/";

        public static void Main(string[] args)
        {
            try
            {
                var channelFactory = new ChannelFactory<IService>(new NetTcpBinding(), new EndpointAddress(Path));
                var proxy = channelFactory.CreateChannel();
                Console.WriteLine(JsonConvert.SerializeObject(proxy.GetSkola(0), Formatting.Indented));
            }
            catch (FaultException<Zmaj> ex)
            {
                Console.WriteLine(ex.Detail.GetCatchPhrase());
            }

            Console.ReadKey();
        }
    }
}