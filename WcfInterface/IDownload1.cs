using System;
using System.ServiceModel;

namespace WcfInterface
{
    [ServiceContract]
    public interface IDownload1
    {
        [OperationContract]
        Download GetFile(Guid guid);
    }
}
