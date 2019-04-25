using System;
using System.Globalization;
using System.Linq.Expressions;
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
                Console.WriteLine(JsonConvert.SerializeObject(proxy.GetSmerovi()));
                Console.WriteLine(JsonConvert.SerializeObject(proxy.GetUcenici()));

                var ucenik = proxy.GetUcenik(7);
                Console.WriteLine(JsonConvert.SerializeObject(ucenik));
                ucenik.DatumRodjenja = new DateTime(2000, 8, 19);
                Console.WriteLine("Rows updated {0}.", proxy.PutUcenik(ucenik));
            }
            catch (FaultException<Komunizam> ex)
            {
                Console.WriteLine(ex.Detail.HraniMePropagandom());
            }
        }
    }
}