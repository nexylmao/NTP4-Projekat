using System;
using System.ServiceModel;
using SkolskiSistemCommon;
using Newtonsoft.Json;

namespace SkolskiSistemClient
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var channelFactory = new ChannelFactory<IService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8000/"));
            var proxy = channelFactory.CreateChannel();

            try
            {
                Console.WriteLine(JsonConvert.SerializeObject(proxy.GetSkole()));
                Console.WriteLine(JsonConvert.SerializeObject(proxy.GetSkola(0)));
                Console.WriteLine(JsonConvert.SerializeObject(proxy.GetSkola(1)));
            }
            catch (FaultException<Komunizam> ex)
            {
                Console.WriteLine(ex.Detail.HraniMePropagandom());
            }
        }
    }
}