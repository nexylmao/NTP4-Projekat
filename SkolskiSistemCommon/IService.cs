using System.Collections.Generic;
using System.ServiceModel;

namespace SkolskiSistemCommon
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [FaultContract(typeof(Komunizam))]
        IEnumerable<Skola> GetSkole();

        [OperationContract]
        [FaultContract(typeof(Komunizam))]
        Skola GetSkola(int id);
    }
}