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
            try
            {
                var channelFactory = new ChannelFactory<IService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8000/"));
                var proxy = channelFactory.CreateChannel();
                Interface.Initialize(proxy);
            }
            catch (FaultException<Komunizam> ex)
            {
                Console.WriteLine(ex.Detail.HraniMePropagandom());
            }
        }
    }
}