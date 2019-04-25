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

        [OperationContract]
        [FaultContract(typeof(Komunizam))]
        Skola PostSkola(Skola skola);

        [OperationContract]
        [FaultContract(typeof(Komunizam))]
        int PutSkola(Skola skola);

        [OperationContract]
        [FaultContract(typeof(Komunizam))]
        IEnumerable<Smer> GetSmerovi();

        [OperationContract]
        [FaultContract(typeof(Komunizam))]
        Smer GetSmer(int id);

        [OperationContract]
        [FaultContract(typeof(Komunizam))]
        Smer PostSmer(Smer smer);

        [OperationContract]
        [FaultContract(typeof(Komunizam))]
        int PutSmer(Smer smer);

        [OperationContract]
        [FaultContract(typeof(Komunizam))]
        IEnumerable<Ucenik> GetUcenici();

        [OperationContract]
        [FaultContract(typeof(Komunizam))]
        Ucenik GetUcenik(int id);

        [OperationContract]
        [FaultContract(typeof(Komunizam))]
        Ucenik PostUcenik(Ucenik ucenik);

        [OperationContract]
        [FaultContract(typeof(Komunizam))]
        int PutUcenik(Ucenik ucenik);
    }
}