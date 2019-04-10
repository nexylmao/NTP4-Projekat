using System;
using System.ServiceModel;

namespace skolski_sistem_common
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        DateTime Now();
    }
}