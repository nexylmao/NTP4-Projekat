using System.Collections.Generic;
using System.ServiceModel;

namespace skolski_sistem_common
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [FaultContract(typeof(Zmaj))]
        IEnumerable<Skola> GetSkole();

        [OperationContract]
        [FaultContract(typeof(Zmaj))]
        Skola GetSkola(int id);

        [OperationContract]
        [FaultContract(typeof(Zmaj))]
        Skola PostSkola(Skola skola);

        [OperationContract]
        [FaultContract(typeof(Zmaj))]
        bool UpdateSkola(Skola skola);

        [OperationContract]
        [FaultContract(typeof(Zmaj))]
        bool DeleteSkola(int id);
    }
}